using ListaDeCompras.Models;

namespace ListaDeCompras.View;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto prod_anex = BindingContext as Produto;

            Produto p = new Produto
            {
                Id = prod_anex.Id,
                Descricao = txtDescricao.Text,
                Categoria = txtCategoria.Text, // Adicionada
                Quantidade = Convert.ToDouble(txtQuantidade.Text),
                Preco = Convert.ToDouble(txtPreco.Text)
            };

            await App.Db.Update(p);
            await DisplayAlert("Sucesso", "Registro Atualizado", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}