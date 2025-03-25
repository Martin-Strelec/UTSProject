using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using UTSProject.Resources.Services;
using UTSProject.Resources.Models;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using System.Diagnostics;

namespace UTSProject.Resources.ViewModels
{
    [QueryProperty(nameof(UserInput), nameof(UserInput))]
    public partial class ConnectionsViewModel : ObservableObject
    {
        [ObservableProperty]
        UserInput _userInput;

        [ObservableProperty]
        private string _tripID;

        [ObservableProperty]
        private string _date;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _vehicleID;

        [ObservableProperty]
        private ObservableCollection<ConnectionDisplayData> _connections;

        [ObservableProperty]
        private bool _indicatorIsVisible;

        [ObservableProperty]
        private bool _connectionsIsVisible;
        [ObservableProperty]
        private bool _buttonIsVisible;

        private readonly NTAService _publicTransportService;

        public ConnectionsViewModel(NTAService publicTransportService)
        {
            _publicTransportService = publicTransportService;
            ConnectionsIsVisible = false;
            ButtonIsVisible = false;
        }

        [RelayCommand]
        async Task LoadAnother()
        {
            // Clear the Collection
            Connections.Clear();
            // Hide the button
            ButtonIsVisible = false;
            // Load new data
            await foreach (var entity in _publicTransportService.InitializeData())
            {
                Connections.Add(new ConnectionDisplayData
                {
                    Date = entity.TripUpdate.Trip.StartDate,
                    TripID = entity.TripUpdate.Trip.TripID,
                    Time = entity.TripUpdate.Trip.StartTime,
                    VehicleID = entity.TripUpdate.Vehicle?.Id
                });
            }
            // Hide the button
            ButtonIsVisible = true;
        }

        public async void LoadData()
        {
            //Showing the loading indicator
            IndicatorIsVisible = true;

            //Initializing Collection
            Connections = new ObservableCollection<ConnectionDisplayData>();

            //Using NTAService method for storing entities
            await _publicTransportService.ReadDataAsync(UserInput.Time);

            Debug.WriteLine(UserInput.Time);
            await foreach (var entity in _publicTransportService.InitializeData())
            {
                Connections.Add(new ConnectionDisplayData
                {
                    Date = entity.TripUpdate.Trip.StartDate,
                    Time = entity.TripUpdate.Trip.StartTime,
                    VehicleID = entity.TripUpdate.Vehicle?.Id
                });
            }

            //Hiding the loading indicator
            IndicatorIsVisible = false;

            //Setting the view to true
            ConnectionsIsVisible = true;

            //Setting the Button to visible
            ButtonIsVisible = true;
        }
    }
}
