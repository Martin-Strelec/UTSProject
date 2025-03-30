using UTSProject.Resources.ViewModels;
using UTSProject.Resources.Models;
using UTSProject.Resources.Services;

namespace UTSProject.Resources.Views;

public partial class ConnectionsPage : ContentPage
{
    private LoadDataService _ld;
    private ConnectionsViewModel _vm;
    public ConnectionsPage(ConnectionsViewModel vm, LoadDataService ld)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
        _ld = ld;
    }

    private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ConnectionDetailsModel selectedItem)
        {
            await Shell.Current.GoToAsync($"/{nameof(ConnectionsPage)}/{nameof(DetailPage)}",
                new Dictionary<string, object>
                {
                    ["ConnectionDetails"] = selectedItem
                });
        }
    }

    //private async void OnBackButtonClicked(object sender, EventArgs e)
    //{
    //    await Shell.Current.GoToAsync($"{nameof(MainPage)}"); // Manually navigate back
    //}

    private void ConnectionsPage_Loaded(object sender, EventArgs e)
    {
        ConnectionsList.SelectedItem = null;
        _vm.Connections = _ld.Connections;
    }
}