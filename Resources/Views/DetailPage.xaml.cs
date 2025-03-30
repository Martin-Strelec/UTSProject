using UTSProject.Resources.Services;
using UTSProject.Resources.ViewModels;
namespace UTSProject.Resources.Views;

public partial class DetailPage : ContentPage
{
    private DetailViewModel _vm;
    private SavedConnectionsService _sc;
    public DetailPage(DetailViewModel vm, SavedConnectionsService sc)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
        _sc = sc;

    }

    private void DetailPage_Loaded(object sender, EventArgs e)
    {
        if (_vm.ConnectionDetails.IsNotSaved)
        {
            SaveButton.IsVisible = true;
            DeleteButton.IsVisible = false;
        }
        else
        {
            SaveButton.IsVisible = false;
            DeleteButton.IsVisible = true;
        }
    }

    async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (_vm.ConnectionDetails.IsNotSaved == false)
        {
            //Remove the connection from Saved connections
            _sc.RemoveFromConnections(_vm.ConnectionDetails);
            //Change the state of the connection to not saved
            _vm.ConnectionDetails.IsNotSaved = true;
            //Change the visibility of the buttons
            DeleteButton.IsVisible = false;
            SaveButton.IsVisible = true;
            //Display alert
            await DisplayAlert("Alert", "Connection has been deleted", "OK");
        }
        else
        {
            await DisplayAlert("Alert", "Cannot remove the saved item", "OK");
        }
    }

    async void SaveButton_Clicked(object sender, EventArgs e)
    {
        //Check if the connection is saved already
        if (_vm.ConnectionDetails.IsNotSaved == true)
        {
            _sc.SaveConnection(_vm.ConnectionDetails);
            _vm.ConnectionDetails.IsNotSaved = false;
            DeleteButton.IsVisible = true;
            SaveButton.IsVisible = false;
            await DisplayAlert("Alert", "Connection has been saved!", "OK");
        }
        else
        {
            await DisplayAlert("Alert", "Connection is already saved!", "OK");
        }
    }
}