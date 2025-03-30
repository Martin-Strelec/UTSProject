using System;
using System.Collections.Generic;
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
    [QueryProperty("ConnectionDetails", "ConnectionDetails")] // Query property used to recieve data from another pages
    public partial class DetailViewModel : ObservableObject
    {
        // Observable properties
        [ObservableProperty]
        private ConnectionDetailsModel _connectionDetails;

        [ObservableProperty]
        private bool _isNotSaved;
    }
}
