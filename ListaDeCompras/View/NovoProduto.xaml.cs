using ListaDeCompras.Models;
using System.Threading.Tasks;

namespace ListaDeCompras.View;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto p = new Produto
			{
				Descricao = txtDescricao.Text,
				Categoria = txtCategoria.Text, // Adicionada
				Quantidade = Convert.ToDouble(txtQuantidade.Text),
				Preco = Convert.ToDouble(txtPreco.Text)
			};

			await App.Db.Insert(p);
			await DisplayAlert("Sucesso", "Registro inserido", "OK");
			await Navigation.PopAsync();
		} 
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
    }
}