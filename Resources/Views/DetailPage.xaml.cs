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

    async void ImageButton_Clicked(object sender, EventArgs e)
    {
        //Check if the connection is saved already
        if (_vm.ConnectionDetails.IsNotSaved == true)
        {
            _sc.SaveConnection(_vm.ConnectionDetails);
            _vm.ConnectionDetails.IsNotSaved = false;
            await DisplayAlert("Alert", "Connection has been saved!", "OK");
        }
        else
        {
            await DisplayAlert("Alert", "Connection is already saved!", "OK");
        }
    }
}