using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using UTSProject.Resources.Services;
using UTSProject.Resources.Models;
using CommunityToolkit.Mvvm.Input;
using System.Globalization;
using System.Diagnostics;

namespace UTSProject.Resources.ViewModels
{
    [QueryProperty(nameof(UserInput), "userInput")]
    public partial class ConnectionsViewModel : ObservableObject
    {
        [ObservableProperty]
        private DateTime _userInput;

        //[ObservableProperty]
        //private string _delay;

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

        private List<Entity> _entities;
        private int _start;
        private int _step;

        private readonly NTAService _publicTransportService;

        public ConnectionsViewModel(NTAService publicTransportService)
        {
            _publicTransportService = publicTransportService;
            _start = 0;
            _step = 25;
            ConnectionsIsVisible = false;
            ButtonIsVisible = false;
            LoadData();
        }

        [RelayCommand]
        async Task LoadAnother()
        {
            // Clear the Collection
            Connections.Clear();
            // Hide the button
            ButtonIsVisible = false;
            // Load new data
            LoadData();
        }

        private async void LoadData()
        {
            //Showing the loading indicator
            IndicatorIsVisible = true;

            //Initializing Collection
            Connections = new ObservableCollection<ConnectionDisplayData>();

            //Using NTAService method for storing entities
            await foreach (var entity in _publicTransportService.ReadDataAsync(_start, _step)) 
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
            
            //Pagination
            this._start += this._step;
        }
        private DateTime ConvertTimeAndDate(ReadOnlySpan<char> time, ReadOnlySpan<char> date)
        {
            string timeFormat = @"hh\:mm\:ss"; // Time Format

            if ((int.TryParse(date.Slice(0, 4), out int year) &&
                 int.TryParse(date.Slice(4, 2), out int month) &&
                 int.TryParse(date.Slice(6, 2), out int day)) &&
                 (TimeOnly.TryParseExact(time, timeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly newTime)))
            {
                return new DateTime(year, month, day, newTime.Hour, newTime.Minute, newTime.Second);
            }
            return DateTime.Now;
        }
    }
}
