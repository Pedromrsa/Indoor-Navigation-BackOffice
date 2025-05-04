using IndoorMappingWebsite.Components.Pages;
using IndoorMappingWebsite.Models;

namespace IndoorMappingWebsite.Services
{
    public interface IPathMapService
    {
        Task<List<Caminho2>> GetPathsAsync();
        Task<bool> CreatePathAsync(Caminho2 path);
        Task<Caminho2> GetPathByIdAsync(int id);
        Task<bool> DeletePathById(int id);
        Task<bool> UpdatePath(Caminho2 Path);
    }
    public class PathMapService : IPathMapService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "/api/Caminhos2";

        public PathMapService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CreatePathAsync(Caminho2 path)
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

        public async Task<Caminho2> GetPathByIdAsync(int id)
        {
           
            try
            {
                return await _httpClient.GetFromJsonAsync<Caminho2>($"{_baseUrl}{id}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Caminho2>> GetPathsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Caminho2>>(_baseUrl);
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

        public async Task<bool> UpdatePath(Caminho2 Path)
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
