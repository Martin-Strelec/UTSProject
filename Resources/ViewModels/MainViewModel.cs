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
        [ObservableProperty]
        private DateTime _selectedDate;
        [ObservableProperty]
        private TimeSpan _selectedTime;

        private readonly NTAService _publicTransportService;

        public MainViewModel(NTAService publicTransportService)
        {
            _selectedDate = DateTime.Now;
            _selectedTime = DateTime.Now.TimeOfDay;
            _publicTransportService = publicTransportService;
        }

        [RelayCommand]
        async Task Search()
        {
            DateTime input = new DateTime(
                SelectedDate.Year,
                SelectedDate.Month,
                SelectedDate.Day,
                SelectedTime.Hours,
                SelectedTime.Minutes,
                SelectedTime.Seconds
            );
            //Debug.WriteLine(input);
            await Shell.Current.GoToAsync($"./{nameof(ConnectionsPage)}?userinput={input}");
        }
    }
}
