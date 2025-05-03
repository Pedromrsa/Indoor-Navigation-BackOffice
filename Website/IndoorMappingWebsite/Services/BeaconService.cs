using IndoorMappingWebsite.Models;

namespace IndoorMappingWebsite.Services
{

    public interface IBeaconService
    {
        Task<List<Beacon>> GetBeaconsAsync();
        Task<bool> CreateBeaconAsync(Beacon beacon);
        Task<Beacon> GetBeaconByIdAsync(int id);
        Task<bool> DeleteBeaconAsync(int id);

        Task<bool> UpdateBeaconAsync(Beacon beacon);
    }
    public class BeaconService : IBeaconService
    {

        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "/api/Beacons";

        public BeaconService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Beacon>> GetBeaconsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Beacon>>(_baseUrl);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> CreateBeaconAsync(Beacon beacon)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, beacon);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<Beacon> GetBeaconByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Beacon>($"{_baseUrl}/{id}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteBeaconAsync(int id)
        {
            try
            { 
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
                // Verifica se a resposta da requisição foi bem-sucedida
                if (response.IsSuccessStatusCode)
                {
                    return true; // Deletado com sucesso
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }

        }

        public async Task<bool> UpdateBeaconAsync(Beacon beacon)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{beacon.id}", beacon);
                if (response.IsSuccessStatusCode)
                {
                    return true; 
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }
    }
}

