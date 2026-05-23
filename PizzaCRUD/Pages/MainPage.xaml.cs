using Microsoft.Maui.Controls;
using System;

namespace PizzaCRUD.Pages;

public partial class MainPage : ContentPage
{
    private readonly Pedidos _repositorio;

    public MainPage(Pedidos repositorio)
    {
        InitializeComponent();
        _repositorio = repositorio;
    }

    private async void BtnCrear_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtCliente.Text) ||
            string.IsNullOrWhiteSpace(txtPizzaNombre.Text) ||
            !decimal.TryParse(txtPizzaPrecio.Text, out decimal precio))
        {
            await DisplayAlert("Error", "Por favor llene todos los campos correctamente. El precio debe ser numérico.", "OK");
            return;
        }

        try
        {
            var result = _repositorio.Create(txtCliente.Text, txtPizzaNombre.Text, precio);
            
            txtId.Text = string.Empty;
            txtCliente.Text = string.Empty;
            txtPizzaNombre.Text = string.Empty;
            txtPizzaPrecio.Text = string.Empty;

            await DisplayAlert("Éxito", $"Pedido {result.Item1.Id} y Pizza {result.Item2.Id} creados correctamente.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void BtnLeer_Clicked(object sender, EventArgs e)
    {
        if (!int.TryParse(txtId.Text, out int id))
        {
            await DisplayAlert("Error", "Ingrese un ID de pedido válido para leer.", "OK");
            return;
        }

        try
        {
            var (pedido, pizza) = _repositorio.ReadById(id);

            if (pedido != null && pizza != null)
            {
                txtCliente.Text = pedido.Cliente;
                txtPizzaNombre.Text = pizza.Nombre;
                txtPizzaPrecio.Text = pizza.Precio.ToString();
            }
            else
            {
                await DisplayAlert("Aviso", "No se encontró el pedido o su detalle.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void BtnActualizar_Clicked(object sender, EventArgs e)
    {
        if (!int.TryParse(txtId.Text, out int id) ||
            string.IsNullOrWhiteSpace(txtCliente.Text) ||
            string.IsNullOrWhiteSpace(txtPizzaNombre.Text) ||
            !decimal.TryParse(txtPizzaPrecio.Text, out decimal precio))
        {
            await DisplayAlert("Error", "Ingrese un ID válido y llene todos los campos. El precio debe ser numérico.", "OK");
            return;
        }

        try
        {
            _repositorio.Update(id, txtCliente.Text, txtPizzaNombre.Text, precio);
            await DisplayAlert("Éxito", "Pedido y Pizza actualizados correctamente.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void BtnBorrar_Clicked(object sender, EventArgs e)
    {
        if (!int.TryParse(txtId.Text, out int id))
        {
            await DisplayAlert("Error", "Ingrese un ID válido para borrar.", "OK");
            return;
        }

        int idGuardado = id;

        try
        {
            _repositorio.Delete(idGuardado);

            txtId.Text = string.Empty;
            txtCliente.Text = string.Empty;
            txtPizzaNombre.Text = string.Empty;
            txtPizzaPrecio.Text = string.Empty;

            await DisplayAlert("Éxito", $"Pedido {idGuardado} borrado correctamente.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}