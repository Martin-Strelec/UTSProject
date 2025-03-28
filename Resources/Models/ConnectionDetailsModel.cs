using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSProject.Resources.Models
{
    public class ConnectionDetailsModel
    {
        public string Time {  get; set; }
        public string Date { get; set; }
        public string RouteShortName { get; set; }
        public string RouteLongName { get; set; }
        public StopModel StartPoint { get; set; }
        public StopModel SearchedPoint { get; set; }
        public StopModel EndPoint { get; set; }
        public string EndPointArrival
        {
            get => EndPoint.ArrivalTime.ToString();
        }
        public string SearchedPointDeparture
        {
            get => SearchedPoint.DepartureTime.ToString();
        }
        public string EndPointName
        {
            get => EndPoint.Name.ToString();
        }
        public string SearchedPointName
        {
            get => SearchedPoint.Name.ToString();
        }
        public List<StopModel> Stops { get; set; }
    }
}
