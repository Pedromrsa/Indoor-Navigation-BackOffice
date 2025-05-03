using IndoorMappingWebsite.Models;
using System.IO;

namespace IndoorMappingWebsite.Services
{
    public interface IAccessibilityService
    {
        Task<List<Accessibility>> GetAccessibilityAsync();
        Task<bool> CreateAccessibilityAsync(Accessibility acc);
        Task<Accessibility> GetAccessibilityByIdAsync(int id);
    }
    public class AccessibilityService : IAccessibilityService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "/api/Acessibilidade";

        public AccessibilityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<bool> CreateAccessibilityAsync(Accessibility acc)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, acc);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Accessibility>> GetAccessibilityAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Accessibility>>(_baseUrl);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<Accessibility> GetAccessibilityByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Accessibility>($"{_baseUrl}{id}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }
    }
}
