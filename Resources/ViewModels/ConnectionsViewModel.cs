using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using UTSProject.Resources.Services;
using UTSProject.Resources.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;

namespace UTSProject.Resources.ViewModels
{
    public partial class ConnectionsViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _date;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private ObservableCollection<Test> _connections;

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
            Connections.Clear();
            UpdateData();
        }

        private void UpdateData()
        {
            for (int i = this._start; i < this._start + this._step; i++)
            {
                Connections.Add(
                    new Test
                    {
                        Date = this._entities[i].TripUpdate.Trip.StartDate,
                        Time = this._entities[i].TripUpdate.Trip.StartTime
                    });
            }
            //Pagination
            this._start += this._step;
        }

        private async void LoadData()
        {
            //Showing the loading indicator
            IndicatorIsVisible = true;

            //Initializing Collection
            Connections = new ObservableCollection<Test>();

            //Using NTAService method for storing entities
            _entities = await _publicTransportService.ReadDataAsync();

            for (int i = this._start; i < this._start+this._step; i++)
            {
                Connections.Add(
                    new Test { 
                        Date = this._entities[i].TripUpdate.Trip.StartDate, 
                        Time = this._entities[i].TripUpdate.Trip.StartTime 
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
    }
}
