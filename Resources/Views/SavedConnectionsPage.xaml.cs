using UTSProject.Resources.ViewModels;
using UTSProject.Resources.Models;
using UTSProject.Resources.Services;
namespace UTSProject.Resources.Views;

public partial class SavedConnectionsPage : ContentPage
{
    // Private properties
    private SavedConnectionsViewModel _vm;
    private SavedConnectionsService _sc;
    public SavedConnectionsPage(SavedConnectionsViewModel vm, SavedConnectionsService sc)
	{
		InitializeComponent();
		BindingContext = vm;
        // DI
        _vm = vm;
        _sc = sc;
	}

    private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ConnectionDetailsModel selectedItem) // Gets the currently selected item
        {
            // Passes the selected item to next page
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}",
                new Dictionary<string, object>
                {
                    ["ConnectionDetails"] = selectedItem
                });
        }
    }

    private void SavedConnectionsPage_Loaded(object sender, EventArgs e)
    {
        _vm.Connections = _sc.GetConnections(); // Passes the connections from SavedConnectionsService to public property
        if (_vm.Connections.Count == 0)
        {
            // Changes the visibility of XAML objects
            EmptyListLabel.IsVisible = true;
            ConnectionsList.IsVisible = false;
        }
        else
        {
            EmptyListLabel.IsVisible = false;
            ConnectionsList.IsVisible = true;
        }
            ConnectionsList.SelectedItem = null; // Resets the selected item
    }
}