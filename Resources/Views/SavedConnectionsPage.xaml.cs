using UTSProject.Resources.ViewModels;
using UTSProject.Resources.Models;
using UTSProject.Resources.Services;
namespace UTSProject.Resources.Views;

public partial class SavedConnectionsPage : ContentPage
{
    private SavedConnectionsViewModel _vm;
    private SavedConnectionsService _sc;
    public SavedConnectionsPage(SavedConnectionsViewModel vm, SavedConnectionsService sc)
	{
		InitializeComponent();
		BindingContext = vm;
        _vm = vm;
        _sc = sc;
	}

    private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ConnectionDetailsModel selectedItem)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}",
                new Dictionary<string, object>
                {
                    ["ConnectionDetails"] = selectedItem
                });
        }
    }

    private void SavedConnectionsPage_Loaded(object sender, EventArgs e)
    {
        _vm.Connections = _sc.GetConnections();
        if (_vm.Connections.Count == 0)
        {
            EmptyListLabel.IsVisible = true;
            ConnectionsList.IsVisible = false;
        }
        else
        {
            EmptyListLabel.IsVisible = false;
            ConnectionsList.IsVisible = true;
        }
            ConnectionsList.SelectedItem = null;
    }
}