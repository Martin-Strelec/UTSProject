using UTSProject.Resources.ViewModels;

namespace UTSProject.Resources.Views;

public partial class ConnectionsPage : ContentPage
{
    private ConnectionsViewModel _vm;
	public ConnectionsPage(ConnectionsViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
		BindingContext = vm;
	}

    private async void OnPageLoaded(object sender, EventArgs e)
    {
        _vm.LoadData();
    }
}