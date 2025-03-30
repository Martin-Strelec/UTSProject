using UTSProject.Resources.ViewModels;

namespace UTSProject.Resources.Views;

public partial class MainPage : ContentPage
{
	// Private properties
	private MainViewModel _vm;
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        // DI
        _vm = vm;
		
	}

    private async void OnPageLoaded(object sender, EventArgs e)
    {
		_vm.SelectedTime = DateTime.Now.TimeOfDay; // Sets the current time
		_vm.ButtonIsVisible = true; // Uncovers the button
		_vm.IndicatorIsRunning = false; // Hides the loading icon
    }

 
}