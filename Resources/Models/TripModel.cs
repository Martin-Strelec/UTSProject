using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSProject.Resources.Models
{
    public class TripModel
    {
        [Column("trip_id")]
        public string TripID { get; set; } 
    }
}
