using UTSProject.Resources.ViewModels;

namespace UTSProject.Resources.Views;

public partial class ConnectionsPage : ContentPage
{
	public ConnectionsPage(ConnectionsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}