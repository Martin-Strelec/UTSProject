using UTSProject.Resources.Views;

namespace UTSProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Registering routes
            Routing.RegisterRoute(nameof(ConnectionsPage), typeof(ConnectionsPage));
            Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
        }
    }
}
