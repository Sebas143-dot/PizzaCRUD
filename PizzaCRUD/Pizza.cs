namespace PizzaCRUD;

public class Pizza
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
}
