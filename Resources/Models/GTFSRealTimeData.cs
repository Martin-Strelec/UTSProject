using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UTSProject.Resources.Models
{
    /*
     * Whole structure of the JSON response can be found at this link: https://developer.nationaltransport.ie/
     * This is subject to change as it was very poorly designed
     */
    public class GtfsRealtimeResponse
    {
        public Header Header { get; set; }
        [JsonProperty("entity")]
        public List<Entity> Entity { get; set; }
    }

    public class Header
    {
        [JsonProperty("gtfs_realtime_version")]
        public required string GtfsRealtimeVersion { get; set; }
    }

    public class Entity
    {
        public required string Id { get; set; }

        [JsonProperty("trip_update")]
        public required TripUpdate TripUpdate { get; set; }
    }

    public class TripUpdate
    {
        public required Trip Trip { get; set; }

        [JsonProperty("stop_time_update")]
        public required List<StopUpdate> StopUpdates { get; set; }

        public required Vehicle Vehicle { get; set; }
    }

    public class Trip
    {
        [JsonProperty("trip_id")]
        public required string TripID { get; set; }

        [JsonProperty("start_time")]
        public required string StartTime { get; set; }

        [JsonProperty("start_date")]
        public required string StartDate { get; set; }

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
        public int? Delay { get; set; }
    }

    public class Departure
    {
        public int? Delay { get; set; }
    }

    public class Vehicle
    {
        public string Id { get; set; }
    }

}
