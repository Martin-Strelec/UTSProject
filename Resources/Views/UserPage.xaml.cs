using UTSProject.Resources.ViewModels;

namespace UTSProject.Resources.Views;

public partial class UserPage : ContentPage
{
	public UserPage(UserViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}