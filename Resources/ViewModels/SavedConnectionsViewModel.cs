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
    [QueryProperty("ConnectionDetails", "Connections")]
    public partial class SavedConnectionsViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ConnectionDetailsModel> _connections;
    }
}
