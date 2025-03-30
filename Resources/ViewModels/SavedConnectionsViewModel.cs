using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using UTSProject.Resources.Models;
using UTSProject.Resources.Services;

namespace UTSProject.Resources.ViewModels
{
    [QueryProperty("ConnectionDetails", "Connections")] // Query property used to recieve data from another pages
    public partial class SavedConnectionsViewModel : ObservableObject
    {
        // Observable properties
        [ObservableProperty]
        private ObservableCollection<ConnectionDetailsModel> _connections;
    }
}
