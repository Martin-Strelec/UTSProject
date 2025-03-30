using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTSProject.Resources.Models;
using UTSProject.Resources.ViewModels;

namespace UTSProject.Resources.Services
{
    public class SavedConnectionsService
    {
        // Private properties
        private ObservableCollection<ConnectionDetailsModel> _connections { get; set; }

        public SavedConnectionsService() 
        {
            _connections = new ObservableCollection<ConnectionDetailsModel>(); // Initializes empty collection
        }

        public void SaveConnection (ConnectionDetailsModel cd) // Adds to connections
        {
            if (_connections != null)
            {
                _connections.Add(cd);
            }
        }
        public ObservableCollection<ConnectionDetailsModel> GetConnections () // Returns connections
        {
            return _connections;
        }
        public void RemoveFromConnections(ConnectionDetailsModel cd) // Removes from connections
        {
            if (_connections != null)
            {
                _connections.Remove(cd);
            }
        }
    }
}
