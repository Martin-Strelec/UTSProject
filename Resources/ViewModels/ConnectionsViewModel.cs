using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using UTSProject.Resources.Services;
using UTSProject.Resources.Models;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using System.Diagnostics;

namespace UTSProject.Resources.ViewModels
{
    [QueryProperty(nameof(Connections), nameof(Connections))] // Query property used for recieving data from another page
    public partial class ConnectionsViewModel : ObservableObject
    {
        // Observable properties
        [ObservableProperty]
        private ObservableCollection<ConnectionDetailsModel> _connections;

        [ObservableProperty]
        private bool _indicatorIsVisible;

        [ObservableProperty]
        private bool _connectionsIsVisible;

        [ObservableProperty]
        private bool _buttonIsVisible;

        // Private properties
        private LoadDataService _ld;

        public ConnectionsViewModel(LoadDataService ld)
        {
            _ld = ld; // assigning di service to a private property
            // Setting default state for XAML objects
            ConnectionsIsVisible = false; 
            ButtonIsVisible = true;
        }

        [RelayCommand]
        async Task LoadAnother()
        {
            // Hide the button
            ButtonIsVisible = false;
            // Add data to ObservableColletion
            Connections =  await _ld.LoadAnother();
            // Hide the button
            ButtonIsVisible = true;
        }
    }
}
