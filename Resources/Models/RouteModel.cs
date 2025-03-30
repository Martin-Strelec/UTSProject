using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSProject.Resources.Models
{
    public class RouteModel
    {
        [Column("route_long_name")]
        public string RouteLongName {  get; set; } // Trip's route long name
        [Column("route_short_name")]
        public string RouteShortName { get; set; } // Trip's route short name
        [Column("agency_name")]
        public string AgencyName {  get; set; } // Agency that operates this route
        [Column("agency_url")]
        public string AgencyURL { get; set; } // Agency URL
    }
}
