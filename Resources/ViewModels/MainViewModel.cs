﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UTSProject.Resources.Models;
using UTSProject.Resources.Services;
using UTSProject.Resources.Views;

namespace UTSProject.Resources.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        // Observable properties
        [ObservableProperty]
        private DateTime _selectedDate;
        [ObservableProperty]
        private TimeSpan _selectedTime;
        [ObservableProperty]
        private bool _buttonIsVisible;
        [ObservableProperty]
        private bool _indicatorIsRunning;
        // Private properties
        private ObservableCollection<ConnectionDetailsModel> _connections;
        private LoadDataService _ld;

        public MainViewModel(LoadDataService ld)
        {
            // Setting the current time for XAML DatePicker and TimePicker
            SelectedDate = DateTime.Now;
            SelectedTime = DateTime.Now.TimeOfDay;
            IndicatorIsRunning = false;
            ButtonIsVisible = true;
            _ld = ld;
        }

        [RelayCommand]
        async Task Search()
        {
            // Instantiate connections
            _connections = new ObservableCollection<ConnectionDetailsModel>();
            // Hide the search button
            ButtonIsVisible = false;
            // Show the Loading indicator
            IndicatorIsRunning = true;
            //Get the user input
            UserInput input = new(SelectedDate, SelectedTime);
            //Load the data
            await _ld.AddConnectionsFromDatabase(input);
            // Navigate to another page
            await Shell.Current.GoToAsync($"{nameof(ConnectionsPage)}");
        }
    }
}
