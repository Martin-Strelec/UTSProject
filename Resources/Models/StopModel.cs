using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSProject.Resources.Models
{
    public class StopModel
    {
        [Column("stop_name")]
        public string Name { get; set; } // Name of the stop
        [Column("stop_lat")]
        public double Latitude { get; set; } // Latitude of the stop's location
        [Column("stop_lon")]
        public double Longtitude { get; set; } // Longtitude of the stop's location
        [Column("arrival_time")]
        public TimeSpan ArrivalTime { get; set; } // Arrival Time
        [Column("departure_time")]
        public TimeSpan DepartureTime { get; set; } // Departure time 
    }
}
