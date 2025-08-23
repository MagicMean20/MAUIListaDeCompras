namespace ListaDeCompras
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        public override void OpenWindow(Window window)
        {
            base.OpenWindow(window);
        }
    }
}
