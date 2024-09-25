using System; // Importa o namespace para classes básicas do .NET
using System.Collections.Generic; // Importa o namespace para listas e coleções genéricas
using System.Linq; // Importa o namespace para funcionalidades de LINQ
using System.Text; // Importa o namespace para manipulação de texto
using System.Threading.Tasks; // Importa o namespace para tarefas assíncronas
using SQLite; // Importa o namespace do SQLite para acesso ao banco de dados

namespace MinhasCompras.Models // Define o namespace para o modelo Produto
{
    public class Produto
    {
        // Campos para armazenar os dados do produto
        string? _descricao;
        double _quantidade;
        double _preco;

        // Propriedade para o ID do produto, que é a chave primária e auto-incrementada
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        // Propriedade para a descrição do produto
        public string? Descricao
        {
            get => _descricao; // Retorna o valor da descrição
            set
            {
                // Verifica se o valor da descrição é nulo
                if (value == null)
                    throw new Exception("Descrição Inválida");

                _descricao = value; // Atribui o valor à descrição
            }
        }

        // Propriedade para a quantidade do produto
        public double Quantidade
        {
            get => _quantidade; // Retorna o valor da quantidade
            set
            {
                // Tenta converter o valor para double e armazena em _quantidade
                if (!double.TryParse(value.ToString(), out _quantidade))
                    _quantidade = 0; // Se a conversão falhar, define como 0

                // Verifica se a quantidade é zero ou negativa
                if (value == 0 || value < 0)
                    throw new Exception("Quantidade Inválida.");

                _quantidade = value; // Atribui o valor à quantidade
            }
        }

        // Propriedade para o preço do produto
        public double Preco
        {
            get => _preco; // Retorna o valor do preço
            set
            {
                // Tenta converter o valor para double e armazena em _preco
                if (!double.TryParse(value.ToString(), out _preco))
                    _preco = value; // Se a conversão falhar, mantém o valor atual

                // Verifica se o preço é menor ou igual a zero
                if (value <= 0)
                    throw new Exception("Preço Inválido.");

                _preco = value; // Atribui o valor ao preço
            }
        }

        // Propriedade que calcula o total do produto (Preço * Quantidade)
        public double Total
        {
            get => Preco * Quantidade; // Retorna o total calculado
        }
    }
}
