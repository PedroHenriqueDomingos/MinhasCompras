using System; // Importa o namespace System, que contém classes básicas do .NET
using System.Collections.Generic; // Importa o namespace para listas e coleções genéricas
using System.Linq; // Importa o namespace para funcionalidades de LINQ
using System.Text; // Importa o namespace para manipulação de texto
using System.Threading.Tasks; // Importa o namespace para tarefas assíncronas
using MinhasCompras.Models; // Importa o namespace das models, onde está a classe Produto
using SQLite; // Importa o namespace do SQLite para acesso ao banco de dados

namespace MinhasCompras.Helpers // Define o namespace para a classe de ajuda do banco de dados
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn; // Declara uma conexão assíncrona com o banco de dados SQLite

        // Construtor que recebe o caminho do banco de dados e inicializa a conexão
        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path); // Inicializa a conexão com o caminho fornecido
            _conn.CreateTableAsync<Produto>().Wait(); // Cria a tabela Produto no banco de dados, se não existir
        }

        // Método para inserir um novo produto no banco de dados
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p); // Chama o método assíncrono para inserir o produto
        }

        // Método para atualizar um produto no banco de dados
        public Task<List<Produto>> Update(Produto p)
        {
            // Define a consulta SQL para atualizar o produto
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE id=?";
            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id); // Executa a consulta e retorna o resultado
        }

        // Método para obter todos os produtos do banco de dados
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync(); // Retorna todos os produtos como uma lista assíncrona
        }

        // Método para deletar um produto pelo seu ID
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id); // Deleta o produto correspondente ao ID fornecido
        }

        // Método para buscar produtos que contêm uma string de pesquisa na descrição
        public Task<List<Produto>> Search(string q)
        {
            // Define a consulta SQL para buscar produtos com base na descrição
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + q + "%'";
            return _conn.QueryAsync<Produto>(sql); // Executa a consulta e retorna o resultado
        }
    }
}
