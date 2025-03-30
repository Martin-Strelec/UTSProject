using UTSProject.Resources.ViewModels;
using UTSProject.Resources.Models;
using UTSProject.Resources.Services;

namespace UTSProject.Resources.Views;

public partial class ConnectionsPage : ContentPage
{
    // private properties
    private LoadDataService _ld;
    private ConnectionsViewModel _vm;
    public ConnectionsPage(ConnectionsViewModel vm, LoadDataService ld)
    {
        InitializeComponent();
        BindingContext = vm;
        // DI
        _vm = vm;
        _ld = ld;
    }

    private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ConnectionDetailsModel selectedItem) // Gets the currently selected item
        {
            // Passes the selected item to next page
            await Shell.Current.GoToAsync($"/{nameof(ConnectionsPage)}/{nameof(DetailPage)}",
                new Dictionary<string, object>
                {
                    ["ConnectionDetails"] = selectedItem
                });
        }
    }

    private void ConnectionsPage_Loaded(object sender, EventArgs e)
    {
        ConnectionsList.SelectedItem = null; // Sets the selected item to null
        _vm.Connections = _ld.Connections; // Loads connections to private property
    }
}