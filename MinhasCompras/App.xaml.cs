using MinhasCompras.Helpers; // Importa a classe de helpers, que provavelmente contém lógica de acesso ao banco de dados

namespace MinhasCompras
{
    public partial class App : Application
    {
        // Declara uma variável estática para o helper de banco de dados
        static SQLiteDatabaseHelper database;

        // Propriedade para acessar a instância do banco de dados
        public static SQLiteDatabaseHelper Database
        {
            get
            {
                // Verifica se a instância do banco de dados já foi criada
                if (database == null)
                {
                    // Define o caminho do banco de dados no armazenamento local
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "arquivo.db3");
                    // Cria uma nova instância do helper de banco de dados com o caminho especificado
                    database = new SQLiteDatabaseHelper(path);
                }
                // Retorna a instância do banco de dados
                return database;
            }
        }

        public App()
        {
            InitializeComponent(); // Inicializa os componentes da aplicação

            // Define a cultura atual para "pt-BR" (Português - Brasil)
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");

            // Define a página principal da aplicação como AppShell
            MainPage = new AppShell();
        }
    }
}
