using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using UTSProject.Resources.Models;

namespace UTSProject.Resources.Services
{
    class NTAService
    {
        private readonly HttpClient _httpClient;
        private const string apiUrl = "https://api.nationaltransport.ie/gtfsr/v2/gtfsr?format=json"; //URI
        private const string apiKey = "f94c4155f38c44cb86eee1acf9032f24"; //API key

        public NTAService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }

        public async Task<List<Connection>> GetConnectionsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Connection>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching connections: {ex.Message}");
            }
            return new List<Connection>();
        }
    }
}
