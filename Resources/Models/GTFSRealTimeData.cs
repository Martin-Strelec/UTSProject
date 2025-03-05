using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UTSProject.Resources.Models
{
    public class Connection
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("trip_update")]
        public TripUpdate TripUpdate { get; set; }
    }
    public class TripUpdate
    {
        [JsonPropertyName("trip")]
        public Trip Trip { get; set; }

        [JsonPropertyName("stop_time_update")]
        public List<StopTimeUpdate> StopTimeUpdate { get; set; }
    }
    public class Trip
    {
        [JsonPropertyName("trip_id")]
        public string TripId { get; set; }

        [JsonPropertyName("start_time")]
        public string StartTime { get; set; }

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }

        [JsonPropertyName("schedule_relationship")]
        public string ScheduleRelationship { get; set; }

        [JsonPropertyName("route_id")]
        public string RouteId { get; set; }

        [JsonPropertyName("direction_id")]
        public int DirectionId { get; set; }
    }
    public class StopTimeUpdate
    {
        [JsonPropertyName("stop_sequence")]
        public int StopSequence { get; set; }

        [JsonPropertyName("arrival")]
        public Delay Arrival { get; set; }

        [JsonPropertyName("departure")]
        public Delay Departure { get; set; }

        [JsonPropertyName("stop_id")]
        public string StopId { get; set; }

        [JsonPropertyName("schedule_relationship")]
        public string ScheduleRelationship { get; set; }
    }
    public class Delay
    {
        [JsonPropertyName("delay")]
        public int DelayValue { get; set; }
    }
}
