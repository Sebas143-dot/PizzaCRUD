using Microsoft.Data.Sqlite;
using Dapper;

namespace PizzaCRUD;

public class Pedidos : IDisposable
{
    private readonly SqliteConnection _connection;

    public Pedidos()
    {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "pizzacrud.db");
        string connectionString = $"Data Source={dbPath}";
        _connection = new SqliteConnection(connectionString);
        _connection.Open();

        _connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Pedido (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Cliente TEXT NOT NULL
            );
            CREATE TABLE IF NOT EXISTS Pizza (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                PedidoId INTEGER NOT NULL,
                Nombre TEXT NOT NULL,
                Precio DECIMAL NOT NULL,
                FOREIGN KEY(PedidoId) REFERENCES Pedido(Id) ON DELETE CASCADE
            );");
    }

    public (Pedido, Pizza) Create(string cliente, string pizzaNombre, decimal pizzaPrecio)
    {
        string sqlPedido = "INSERT INTO Pedido (Cliente) VALUES (@Cliente); SELECT last_insert_rowid();";
        int pedidoId = _connection.ExecuteScalar<int>(sqlPedido, new { Cliente = cliente });

        string sqlPizza = "INSERT INTO Pizza (PedidoId, Nombre, Precio) VALUES (@PedidoId, @Nombre, @Precio); SELECT last_insert_rowid();";
        int pizzaId = _connection.ExecuteScalar<int>(sqlPizza, new { PedidoId = pedidoId, Nombre = pizzaNombre, Precio = pizzaPrecio });

        var pedido = new Pedido { Id = pedidoId, Cliente = cliente };
        var pizza = new Pizza { Id = pizzaId, PedidoId = pedidoId, Nombre = pizzaNombre, Precio = pizzaPrecio };

        return (pedido, pizza);
    }

    public (Pedido?, Pizza?) ReadById(int id)
    {
        var pedido = _connection.QueryFirstOrDefault<Pedido>("SELECT * FROM Pedido WHERE Id = @Id", new { Id = id });
        if (pedido == null) return (null, null);

        var pizza = _connection.QueryFirstOrDefault<Pizza>("SELECT * FROM Pizza WHERE PedidoId = @Id", new { Id = id });
        return (pedido, pizza);
    }

    public List<(Pedido, Pizza?)> ReadAll()
    {
        var result = new List<(Pedido, Pizza?)>();
        var pedidos = _connection.Query<Pedido>("SELECT * FROM Pedido").ToList();
        
        foreach (var p in pedidos)
        {
            var pizza = _connection.QueryFirstOrDefault<Pizza>("SELECT * FROM Pizza WHERE PedidoId = @Id", new { Id = p.Id });
            result.Add((p, pizza));
        }
        
        return result;
    }

    public void Update(int id, string cliente, string pizzaNombre, decimal pizzaPrecio)
    {
        int affectedPedido = _connection.Execute("UPDATE Pedido SET Cliente = @Cliente WHERE Id = @Id", new { Cliente = cliente, Id = id });
        if (affectedPedido == 0) throw new Exception("Pedido no encontrado.");

        _connection.Execute("UPDATE Pizza SET Nombre = @Nombre, Precio = @Precio WHERE PedidoId = @Id", new { Nombre = pizzaNombre, Precio = pizzaPrecio, Id = id });
    }

    public void Delete(int id)
    {
        _connection.Execute("DELETE FROM Pizza WHERE PedidoId = @Id", new { Id = id });
        int affected = _connection.Execute("DELETE FROM Pedido WHERE Id = @Id", new { Id = id });
        if (affected == 0) throw new Exception("Pedido no encontrado.");
    }

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
    }
}
