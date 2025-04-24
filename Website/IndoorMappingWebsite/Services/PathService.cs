using IndoorMappingWebsite.Models;

namespace IndoorMappingWebsite.Services
{
    public interface IPathService
    {
        Task<List<Models.Path>> GetPathsAsync();
        Task<bool> CreatePathAsync(Models.Path path);
        Task<Models.Path> GetPathByIdAsync(int id);
    }
    public class PathService : IPathService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "------";

        public PathService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CreatePathAsync(Models.Path path)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, path);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<Models.Path> GetPathByIdAsync(int id)
        {
           
            try
            {
                return await _httpClient.GetFromJsonAsync<Models.Path>($"{_baseUrl}{id}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Models.Path>> GetPathsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Models.Path>>(_baseUrl);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }
    }
}
