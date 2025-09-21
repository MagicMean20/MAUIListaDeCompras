using ListaDeCompras.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ListaDeCompras.View;

public partial class ListaProduto : ContentPage
{
	/*Esse recurso tem uma integração de dados com a interface,
		onde ao modificar um dado da ListView ou dessa Collection, a outra parte terá o dado apagado*/
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

	public ListaProduto()
	{
		InitializeComponent();

		//Aqui se faz a integração com a interface
		listProdutos.ItemsSource = lista;
	}

	//Sempre ocorre quando uma tela aparece, independente das funções do construtor
    protected async override void OnAppearing()
    {
		try
		{
			/*A inserção dos dados não pode ser feita de forma direta,
			sendo necessário criar uma <List> para inserir os registros na Collection*/
			List<Produto> tnp = await App.Db.GetAll();

			//Lê a lista e adiciona os registros a cada i (linha)
			tnp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
        }
        
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
		try
		{
			//serve para obter o novo valor da mudança de texto 
			string g = e.NewTextValue;

			//Apaga toda a lista, mas não apaga os dados
			lista.Clear();

			//Mesmo que a inserção de antes, mas essa faz pesquisa
			List<Produto> tnp = await App.Db.Search(g);
            List<Produto> t = await App.Db.Categoria(g); /*Parte adicionada*/

            //Ao buscar um valor, o adiciona na List
            tnp.ForEach(i => lista.Add(i));
            t.ForEach(i => lista.Add(i)); /*Parte adicionada*/

            /*
			Em ordem mais explícita:
				1- limpando a lista (apenas para a busca);
				2- Procurando o item;
				3- Adicionando o novo item pesquisado.
			*/
        }
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
		finally
		{
			listProdutos.IsRefreshing = true;
		}
        
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
		double soma = lista.Sum(i => i.Total);

		string msg = $"O total é {soma:C}";

		DisplayAlert("Total dos Produtos",msg,"OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
		try
		{
            //Identifica o item clicado
            MenuItem select = sender as MenuItem;

            //Obtém o objeto Produto da linha clicada
            Produto p = select.BindingContext as Produto;

            //Pergunta se realmente quer apagar o item
            bool conf = await DisplayAlert("Atenção", $"Confirma a exclusão do produto {p.Descricao}?", "Sim", "Não");

            //Se confirmou, apaga o item do banco de dados, da lista e da Collection
            if (conf)
			{
				await App.Db.Delete(p.Id);
				lista.Remove(p);
            }
		}
		catch(Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void listProdutos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		try
		{
			Produto p = e.SelectedItem as Produto;

			Navigation.PushAsync(new View.EditarProduto { BindingContext = p });
        }
		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void listProdutos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            /*A inserção dos dados não pode ser feita de forma direta,
			sendo necessário criar uma <List> para inserir os registros na Collection*/
            List<Produto> tnp = await App.Db.GetAll();

            //Lê a lista e adiciona os registros a cada i (linha)
            tnp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
		finally // Propriedade que é executado de toda maneira
		{
			listProdutos.IsRefreshing = false;
		}
    }
}