using UTSProject.Resources.ViewModels;

namespace UTSProject.Resources.Views;

public partial class DetailPage : ContentPage
{
	public DetailPage(ConnectionsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}