namespace MinhasCompras.Views; // Define o namespace para as views do aplicativo
using MinhasCompras.Models; // Importa o namespace das models, onde est� a classe Produto

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent(); // Inicializa os componentes da p�gina
    }

    // Evento acionado quando um item da barra de ferramentas � clicado
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Cria um novo objeto Produto com os dados inseridos pelo usu�rio
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text, // Obt�m a descri��o do campo de texto
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // Converte a quantidade para double
                Preco = Convert.ToDouble(txt_preco.Text), // Converte o pre�o para double
            };

            // Insere o novo produto no banco de dados
            await App.Database.Insert(p);
            // Exibe uma mensagem de sucesso ao usu�rio
            await DisplayAlert("Sucesso", "Produto Adicionado", "OK");
            // Navega de volta para a p�gina principal
            await Navigation.PushAsync(new MainPage());
        }
        catch (Exception ex)
        {
            // Em caso de erro, exibe uma mensagem 
            await DisplayAlert("Ops", ex.Message, "Fechar");
        }
    }
}
