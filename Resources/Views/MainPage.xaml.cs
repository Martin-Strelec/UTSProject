using UTSProject.Resources.ViewModels;

namespace UTSProject.Resources.Views;

public partial class MainPage : ContentPage
{
	private MainViewModel _vm;
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = vm;
	}

    private async void OnPageLoaded(object sender, EventArgs e)
    {
		_vm.SelectedTime = DateTime.Now.TimeOfDay;
		_vm.ButtonIsVisible = true;
		_vm.IndicatorIsRunning = false;
    }


}