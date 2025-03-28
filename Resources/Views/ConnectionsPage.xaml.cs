using UTSProject.Resources.ViewModels;
using UTSProject.Resources.Models;

namespace UTSProject.Resources.Views;

public partial class ConnectionsPage : ContentPage
{
    public ConnectionsPage(ConnectionsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ConnectionDetailsModel selectedItem)
        {
            await Shell.Current.GoToAsync($"{nameof(ConnectionsPage)}/{nameof(DetailPage)}",
                new Dictionary<string, object>
                {
                    ["ConnectionDetails"] = selectedItem
                });
        }
    }
}