using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSProject.Resources.Models
{
    public class ConnectionDisplayData
    {
        public string Time {  get; set; }
        public string Date { get; set; }
        private string _vehicleID;
        public string VehicleID
        {
            get => _vehicleID;
            set
            {
                if (value == null)
                {
                    _vehicleID = "?";
                }
                else
                {
                    _vehicleID = value;
                }

            }
        }
    }
}
