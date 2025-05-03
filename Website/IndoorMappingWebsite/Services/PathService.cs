using IndoorMappingWebsite.Components.Pages;
using IndoorMappingWebsite.Models;

namespace IndoorMappingWebsite.Services
{
    public interface IPathService
    {
        Task<List<Caminho>> GetPathsAsync();
        Task<bool> CreatePathAsync(CaminhoPost path);
        Task<Caminho> GetPathByIdAsync(int id);
        Task<bool> DeletePathById(int id);
        Task<bool> UpdatePath(CaminhoPost Path);
    }
    public class PathService : IPathService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "/api/Caminhos";

        public PathService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CreatePathAsync(CaminhoPost path)
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

        public async Task<Caminho> GetPathByIdAsync(int id)
        {
           
            try
            {
                return await _httpClient.GetFromJsonAsync<Caminho>($"{_baseUrl}{id}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Caminho>> GetPathsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Caminho>>(_baseUrl);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeletePathById(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdatePath(CaminhoPost Path)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{Path.id}", Path);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
