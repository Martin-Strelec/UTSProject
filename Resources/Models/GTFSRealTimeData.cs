using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UTSProject.Resources.Models
{ 
    public class GtfsRealtimeResponse
    {
        public List<Entity> Entity { get; set; }
    }

    public class Entity
    {
        public string Id { get; set; }

        [JsonProperty("trip_update")]
        public TripUpdate TripUpdate { get; set; }
    }

    public class TripUpdate
    {
        public Trip Trip { get; set; }

        [JsonProperty("stop_time_update")]
        public List<StopUpdate> StopUpdates { get; set; }

        public Vehicle Vehicle { get; set; }
    }

    public class Trip
    {
        [JsonProperty("trip_id")]
        public string TripID { get; set; }

        [JsonProperty("start_time")]
        public string StartTime { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("route_id")]
        public string RouteID { get; set; }

        [JsonProperty("direction_id")]
        public int Direction { get; set; }

        [JsonProperty("schedule_relationship")]
        public string ScheduleRelationship { get; set; }
    }

    public class StopUpdate
    {
        [JsonProperty("stop_sequence")]
        public int StopSequence { get; set; }

        public Arrival Arrival { get; set; }
        public Departure Departure { get; set; }

        [JsonProperty("stop_id")]
        public string StopID { get; set; }

        [JsonProperty("schedule_relationship")]
        public string ScheduleRelationship { get; set; }
    }

    public class Arrival
    {
        public int? Delay { get; set; } // Nullable to handle missing values
    }

    public class Departure
    {
        public int? Delay { get; set; } // Nullable to handle missing values
    }

    public class Vehicle
    {
        public string Id { get; set; }
    }

}
