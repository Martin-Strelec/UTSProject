using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSProject.Resources.Models
{
    public class ConnectionDetailsModel
    {
        
        public TimeSpan Time {  get; set; } // Time when the Connection was searched
        public DateTime Date { get; set; } // Time when the Connection was searched
        public string RouteShortName { get; set; } // Short Name of the Route 
        public string RouteLongName { get; set; } // Long Name of the Route
        public bool IsNotSaved { get; set; } // Bool if the connection was saved or not
        public StopModel SearchedStop { get; set; } // Searched Stop 
        public StopModel EndStop { get; set; } // End Stop of the trip
        public List<StopModel> Stops { get; set; } // All the stops in the trip
    }
}
