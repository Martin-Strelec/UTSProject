using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using UTSProject.Resources.Services;
using UTSProject.Resources.Models;

namespace UTSProject.Resources.ViewModels
{
    class ConnectionsPageViewModel : ObservableObject
    {
        private readonly NTAService _service;

        public ObservableCollection<Connection> Connections { get; set; } = new();

        public ConnectionsPageViewModel(NTAService service, ObservableCollection<Connection> connections)
        {
            _service = service;
            Connections = connections;
        }

        private async void LoadConnections()
        {
            var connections = await _service.GetConnectionsAsync();
            foreach (var connection in connections)
            {
                Connections.Add(connection);
            }
        }
    }
}
