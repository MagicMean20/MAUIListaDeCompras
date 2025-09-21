using ListaDeCompras.Helpers;
using ListaDeCompras.View;
using System.Globalization;

namespace ListaDeCompras
{
    public partial class App : Application
    {

        static SQLiteDatabaseHelper _db;

        internal static SQLiteDatabaseHelper Db
        {
            get
            {
                if (_db == null)
                {
                    string dbPath = Path.Combine
                        (Environment.GetFolderPath
                        (Environment.SpecialFolder.LocalApplicationData),
                        "Banco-produtos.db3");

                    _db = new SQLiteDatabaseHelper(dbPath);
                }

                return _db;
            }
        }

        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new View.ListaProduto());

           Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var wind = base.CreateWindow(activationState);

            wind.Width = 550;
            wind.Height = 600;

            return wind;
        }
    }
}
