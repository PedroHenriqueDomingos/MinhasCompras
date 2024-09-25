using System.Collections.ObjectModel; // Importa a classe ObservableCollection
using MinhasCompras.Models; // Importa o namespace das models do aplicativo

namespace MinhasCompras
{
    public partial class MainPage : ContentPage
    {
        // Cria uma coleção observável para armazenar produtos
        ObservableCollection<Produto> lista_produtos = new ObservableCollection<Produto>();

        public MainPage()
        {
            InitializeComponent(); // Inicializa os componentes da página
            lst_produtos.ItemsSource = lista_produtos; // Define a fonte de dados para a lista de produtos
        }

        // Evento acionado quando um botão da barra de ferramentas é clicado
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
           
        }

        // Evento acionado quando o texto do campo de pesquisa é alterado
        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string q = e.NewTextValue; // Obtém o novo valor do texto
            lista_produtos.Clear(); // Limpa a lista atual de produtos

            // Busca produtos no banco de dados que correspondem à pesquisa
            List<Produto> tmp = await App.Database.Search(q);
            foreach (Produto p in tmp)
            {
                lista_produtos.Add(p); // Adiciona os produtos encontrados à lista
            }
        }

        // Evento acionado quando um item da lista é selecionado
        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Produto? p = e.SelectedItem as Produto; // Obtém o produto selecionado

            // Navega para a página de edição do produto, passando o produto como contexto
            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p
            });
        }

        // Evento acionado quando o botão do menu é clicado
        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem selecionado = (MenuItem)sender; // Obtém o item do menu clicado
                Produto p = selecionado.BindingContext as Produto; // Obtém o produto correspondente
                bool confirm = await DisplayAlert("Tem Certeza ?", "Remover Produto", "Sim", "Não"); // Confirmação para remoção

                if (confirm)
                {
                    await App.Database.Delete(p.Id); // Remove o produto do banco de dados
                    await DisplayAlert("Sucesso!", "Produto Removido", "OK"); // Mensagem de sucesso
                    lista_produtos.Remove(p); // Remove o produto da lista 
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "Fechar"); // Mensagem de erro, se ocorrer
            }
        }

        // Evento para somar o total dos produtos
        private void ToolbarItem_Clicked_Somar(object sender, EventArgs e)
        {
            double soma = lista_produtos.Sum(i => i.Total); // Calcula a soma dos totais dos produtos
            string msg = $"O Total dos Produtos é {soma:C}"; // Formata a mensagem
            DisplayAlert("Resultado", msg, "Fechar"); // Exibe a mensagem com o total
        }

        // Evento acionado ao clicar no botão para adicionar um novo produto
        private async void ToolbarItem_Clicked_Add(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.NovoProduto()); // Navega para a página de novo produto
        }

        // Método que é chamado quando a página aparece
        protected async override void OnAppearing()
        {
            if (lista_produtos.Count == 0) // Verifica se a lista de produtos está vazia
            {
                List<Produto> tmp = await App.Database.GetAll(); // Obtém todos os produtos do banco de dados
                foreach (Produto p in tmp)
                {
                    lista_produtos.Add(p); // Adiciona os produtos à lista
                }
            }
        }
    }
}
