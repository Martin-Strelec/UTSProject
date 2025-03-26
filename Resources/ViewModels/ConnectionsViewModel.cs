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
        private string _date;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _routeShortName;

        [ObservableProperty]
        private string _routeLongName;

        [ObservableProperty]
        private ObservableCollection<ConnectionDisplayData> _connections;

        [ObservableProperty]
        private bool _indicatorIsVisible;

        [ObservableProperty]
        private bool _connectionsIsVisible;
        [ObservableProperty]
        private bool _buttonIsVisible;

        private readonly NTAService _publicTransportService;
        private readonly DbService _db;

        public ConnectionsViewModel(NTAService publicTransportService, DbService db)
        {
            _publicTransportService = publicTransportService;
            _db = db;
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
            // Add data to ObservableColletion
            AddToConnections();
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

            //Debug.WriteLine(UserInput.Time);
            // Add data to ObservableColletion
            AddToConnections();

            //Hiding the loading indicator
            IndicatorIsVisible = false;

            //Setting the view to true
            ConnectionsIsVisible = true;

            //Setting the Button to visible
            ButtonIsVisible = true;
        }

        private async void AddToConnections()
        {
            await foreach (var entity in _publicTransportService.InitializeData())
            {
                string tripID = entity.TripUpdate.Trip.TripID;

                if (tripID == null)
                {
                    Debug.WriteLine("Skipping due to tripID being null");
                    continue;
                }

                try
                {
                    var routeTask = _db.GetRouteDetails(tripID);
                    var stopsTask = _db.GetTripStops(tripID);

                    await Task.WhenAll(routeTask, stopsTask); // Run both tasks in parallel

                    var dbRouteDetails = routeTask.Result;
                    var dbStops = stopsTask.Result;

                    if (dbRouteDetails == null || dbStops.Count == 0)
                    {
                        Debug.WriteLine($"Missing data for TripID: {entity.TripUpdate.Trip.TripID}");
                        continue; // Skip this iteration
                    }

                    Connections.Add(new ConnectionDisplayData
                    {
                        Date = entity.TripUpdate.Trip.StartDate,
                        Time = entity.TripUpdate.Trip.StartTime,
                        RouteShortName = dbRouteDetails.RouteShortName,
                        RouteLongName = dbRouteDetails.RouteLongName,
                        StartPoint = dbStops[0].Name,
                        EndPoint = dbStops[dbStops.Count - 1].Name
                    });
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Error with adding Connections: {e}");
                }

                
            }
        }
    }
}
