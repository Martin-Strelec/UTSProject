using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            UserInput input = new UserInput(SelectedDate, SelectedTime);

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"userInput", input}
            };

            await Shell.Current.GoToAsync("ConnectionsPage", parameters);
        }
    }
}
