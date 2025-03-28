using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using UTSProject.Resources.Services;
using UTSProject.Resources.Models;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using System.Diagnostics;

namespace UTSProject.Resources.ViewModels
{
    [QueryProperty(nameof(Connections), nameof(Connections))]
    public partial class ConnectionsViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ConnectionDetailsModel> _connections;

        [ObservableProperty]
        private bool _indicatorIsVisible;

        [ObservableProperty]
        private bool _connectionsIsVisible;

        [ObservableProperty]
        private bool _buttonIsVisible;

        private LoadDataService _ld;

        public ConnectionsViewModel(LoadDataService ld)
        {
            _ld = ld;
            ConnectionsIsVisible = false;
            ButtonIsVisible = true;
        }

        [RelayCommand]
        async Task LoadAnother()
        {
            // Hide the button
            ButtonIsVisible = false;
            // Add data to ObservableColletion
            Connections =  await _ld.LoadAnother(Connections);
            // Hide the button
            ButtonIsVisible = true;
        }
    }
}
