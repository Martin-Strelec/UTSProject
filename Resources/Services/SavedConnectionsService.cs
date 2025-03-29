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
        private List<ConnectionDetailsModel> _connections { get; set; }

        public SavedConnectionsService()
        {
            _connections = new List<ConnectionDetailsModel>();
        }

        public void SaveConnection (ConnectionDetailsModel cd)
        {
            if (_connections != null)
            {
                _connections.Add(cd);
            }
        }
        public List<ConnectionDetailsModel> GetConnections ()
        {
            return _connections;
        }

        public void RemoveFromConnections()
        {
        }
    }
}
