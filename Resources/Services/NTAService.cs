using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UTSProject.Resources.Models;

namespace UTSProject.Resources.Services
{
    public class NTAService
    {
        // DI
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        // Private properties
        private readonly string apiUri = "https://api.nationaltransport.ie/gtfsr/v2/gtfsr?format=json";
        private readonly string apiKey = Environment.GetEnvironmentVariable("api_key");
        private readonly DbService _dbservice;
        private static int _paginationStart = 0;
        private static int _paginationStep = 25;
        //Public properties
        public List<Entity> Items { get; private set; }
        public GtfsRealtimeResponse Response { get; private set; }

        public NTAService()
        {
            //Initializing HTTP Client
            _client = new HttpClient();
            //Initializing Database
            _dbservice = new DbService();
            //Initializing Pagination 
            _paginationStart = 0;
            _paginationStep = 25;
        }

        public async Task<GtfsRealtimeResponse> ReadDataAsync(TimeSpan time)
        {
            Response = new GtfsRealtimeResponse();

            try
            {
                //Request header
                _client.DefaultRequestHeaders.Add("x-api-key", Environment.GetEnvironmentVariable(apiKey));

                //Request method
                HttpResponseMessage response = await _client.GetAsync(apiUri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Response = JsonConvert.DeserializeObject<GtfsRealtimeResponse>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            for (int i = 0; i < Response.Entity.Count; i++)
            {
                if (TimeSpan.Parse(Response.Entity[i].TripUpdate.Trip.StartTime) > time)
                {
                    _paginationStart = i;
                    break;
                }
            }

            return Response;
            //var response2 = await _dbservice.GetTripStops("4538_114450");
        }

        public async IAsyncEnumerable<Entity> InitializeData()
        {
            foreach (var entity in Response.Entity.Skip(_paginationStart).Take(_paginationStep))
            {
                yield return entity;
            }
            //Increment pagination
            _paginationStart += _paginationStep;
        }
    }
}
