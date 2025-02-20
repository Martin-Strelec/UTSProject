using UTSProject.Resources.ViewModels;

namespace UTSProject.Resources.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}