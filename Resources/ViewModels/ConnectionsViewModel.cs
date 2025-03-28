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
        private string _routeShortName;

        [ObservableProperty]
        private string _searchedPointDeparture;

        [ObservableProperty]
        private string _endPointArrival;

        [ObservableProperty]
        private string _endPointName;

        [ObservableProperty]
        private string _searchedPointName;

        [ObservableProperty]
        private ObservableCollection<ConnectionDetailsModel> _connections;

        [ObservableProperty]
        private bool _indicatorIsVisible;

        [ObservableProperty]
        private bool _connectionsIsVisible;
        [ObservableProperty]
        private bool _buttonIsVisible;

        private LoadDataService _ld;
        private DbService _db;

        public ConnectionsViewModel(LoadDataService ld, DbService db)
        {
            _ld = ld;
            _db = db;
            ConnectionsIsVisible = false;
            ButtonIsVisible = true;
        }

        [RelayCommand]
        async Task LoadAnother()
        {
            // Clear the Collection
            Connections.Clear();
            // Hide the button
            ButtonIsVisible = false;
            // Add data to ObservableColletion
            //await _ld.AddConnectionsFromDatabase(_connections);
            // Hide the button
            ButtonIsVisible = true;
        }
    }
}
