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
        public string RouteLongName {  get; set; }
        [Column("route_short_name")]
        public string RouteShortName { get; set; }
        [Column("agency_name")]
        public string AgencyName {  get; set; }
        [Column("agency_url")]
        public string AgencyURL { get; set; }
    }
}
