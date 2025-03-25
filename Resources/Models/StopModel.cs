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
        public string Name { get; set; }
        [Column("stop_lat")]
        public double Latitude { get; set; }
        [Column("stop_lon")]
        public double Longtitude { get; set; }
        [Column("arrival_time")]
        public TimeSpan ArrivalTime { get; set; }
        [Column("departure_time")]
        public TimeSpan DepartureTime { get; set; }
    }
}
