using MinhasCompras.Models; // Importa o namespace das models, onde est� a classe Produto

namespace MinhasCompras.Views; // Define o namespace da view EditarProduto

public partial class EditarProduto : ContentPage
{
    public EditarProduto()
    {
        InitializeComponent(); // Inicializa os componentes da p�gina
    }

    // Evento acionado quando um item da barra de ferramentas � clicado
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obt�m o produto que est� anexado ao contexto da p�gina
            Produto produto_anexado = BindingContext as Produto;

            // Cria um novo objeto Produto com os dados atualizados
            Produto p = new Produto()
            {
                Id = produto_anexado.Id, // Mant�m o ID do produto original
                Descricao = txt_descricao.Text, // Obt�m a descri��o do campo de texto
                Preco = Convert.ToDouble(txt_preco.Text), // Converte o o pre�o para double
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // Converte a quantidade para double
            };

            // Atualiza o produto no banco de dados
            await App.Database.Update(p);
            // Exibe uma mensagem de sucesso
            await DisplayAlert("Sucesso", "Atualizado", "OK");
            // Navega de volta para a p�gina principal
            await Navigation.PushAsync(new MainPage());
        }
        catch (Exception ex)
        {
            // Em caso de erro, exibe uma mensagem com a descri��o do erro
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
