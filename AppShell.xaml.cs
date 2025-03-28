using UTSProject.Resources.Views;

namespace UTSProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Registering routes
            //Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            //Routing.RegisterRoute(nameof(UserPage), typeof(UserPage));
            Routing.RegisterRoute(nameof(ConnectionsPage), typeof(ConnectionsPage));
            Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
        }
    }
}
