using UTSProject.Resources.ViewModels;
using UTSProject.Resources.Models;

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

    private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ConnectionDetailsModel selectedItem)
        {
            var parameters = new Dictionary<string, object>
        {
            { "ConnectionDetails", selectedItem}
        };
            await Shell.Current.GoToAsync($"{nameof(ConnectionsPage)}/{nameof(DetailPage)}", parameters);
        }
    }
}