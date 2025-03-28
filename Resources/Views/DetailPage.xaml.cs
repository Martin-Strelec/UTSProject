using UTSProject.Resources.ViewModels;
namespace UTSProject.Resources.Views;

public partial class DetailPage : ContentPage
{

    public DetailPage(DetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}