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
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        private readonly string apiUri = "https://api.nationaltransport.ie/gtfsr/v2/gtfsr?format=json";
        private readonly string apiKey = Environment.GetEnvironmentVariable("api_key");
       
        public List<Entity> Items { get; private set; }
        public GtfsRealtimeResponse Response { get; private set; }

        public NTAService()
        { 
            //Initializing HTTP Client
          _client = new HttpClient();
        }

        public async IAsyncEnumerable<Entity> ReadDataAsync(int start, int step)
        {
            Response = new GtfsRealtimeResponse();

            try { 
            
                //Request header
                _client.DefaultRequestHeaders.Add("x-api-key", Environment.GetEnvironmentVariable("api_key"));

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

            foreach (var entity in Response.Entity.Skip(start).Take(step))
            {
                yield return entity;
            }
        }
    }
}
