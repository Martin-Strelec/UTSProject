using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSProject.Resources.Models
{
    public class ConnectionDetailsModel
    {
        public TimeSpan Time {  get; set; }
        public DateTime Date { get; set; }
        public string RouteShortName { get; set; }
        public string RouteLongName { get; set; }
        public StopModel SearchedStop { get; set; }
        public StopModel EndStop { get; set; }
        public List<StopModel> Stops { get; set; }
    }
}
