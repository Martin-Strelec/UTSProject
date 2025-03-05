using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using UTSProject.Resources.Services;
using UTSProject.Resources.Models;
using Android.Net.Wifi.Aware;

namespace UTSProject.Resources.ViewModels
{
    public partial class ConnectionsViewModel : ObservableObject
    {
        private readonly NTAService _publicTransportService;

        public ObservableCollection<Connection> Connections { get; set; } = new();

        public ConnectionsViewModel(NTAService publicTransportService)
        {
            _publicTransportService = publicTransportService;
            LoadConnections();
        }

        private async void LoadConnections()
        {
            var movies = await _publicTransportService.GetConnectionsAsync();
            foreach (var Connection in Connections)
            {
                Connections.Add(Connection);
            }
        }
    }
}
