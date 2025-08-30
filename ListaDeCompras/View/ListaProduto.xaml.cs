using ListaDeCompras.Models;
using System.Collections.ObjectModel;

namespace ListaDeCompras.View;

public partial class ListaProduto : ContentPage
{
	/*Esse recurso tem uma integra��o de dados com a interface,
		onde ao modificar um dado da ListView ou dessa Collection, a outra parte ter� o dado apagado*/
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

	public ListaProduto()
	{
		InitializeComponent();

		//Aqui se faz a integra��o com a interface
		listProdutos.ItemsSource = lista;
	}

	//Sempre ocorre quando uma tela aparece, independente das fun��es do construtor
    protected async override void OnAppearing()
    {
		/*A inser��o dos dados n�o pode ser feita de forma direta,
			sendo necess�rio criar uma <List> para inserir os registros na Collection*/
		List<Produto> tnp = await App.Db.GetAll();

		//L� a lista e adiciona os registros a cada i (linha)
		tnp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PushAsync(new View.NovoProduto());
		}
		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}

    }

    private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
		//serve para obter o novo valor da mudan�a de texto 
		string g = e.NewTextValue;

		//Apaga toda a lista, mas n�o apaga os dados
		lista.Clear();

		//Mesmo que a inser��o de antes, mas essa faz pesquisa
        List<Produto> tnp = await App.Db.Search(g);

		//Ao buscar um valor, o adiciona na List
        tnp.ForEach(i => lista.Add(i));

		/*
		 Em ordem mais expl�cita:
			1- limpando a lista (apenas para a busca);
			2- Procurando o item;
			3- Adicionando o novo item pesquisado.
		 */
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
		double soma = lista.Sum(i => i.Total);

		string msg = $"O total � {soma:C}";

		DisplayAlert("Total dos Produtos",msg,"OK");
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
}