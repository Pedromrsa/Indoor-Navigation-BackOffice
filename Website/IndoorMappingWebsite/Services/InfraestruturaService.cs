using IndoorMappingWebsite.Components.Pages;
using IndoorMappingWebsite.Models;

namespace IndoorMappingWebsite.Services
{
    public interface IInfraestruturaService
    {
        Task<List<Infraestrutura>> GetInfraestruturasAsync();
        Task<bool> CreateInfraestruturaAsync(Infraestrutura path);
        Task<Infraestrutura> GetInfraestruturaByIdAsync(int id);
        Task<bool> DeleteInfraestruturaById(int id);
        Task<bool> UpdateInfraestrutura(Infraestrutura Path);
    }
    public class InfraestruturaService : IInfraestruturaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "/api/Infraestrutura";
        private readonly string _baseUrlGetall= "/api/Infraestrutura/GetAll";

        public InfraestruturaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CreateInfraestruturaAsync(Infraestrutura path)
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

        public async Task<Infraestrutura> GetInfraestruturaByIdAsync(int id)
        {
           
            try
            {
                return await _httpClient.GetFromJsonAsync<Infraestrutura>($"{_baseUrl}{id}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Infraestrutura>> GetInfraestruturasAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Infraestrutura>>(_baseUrlGetall);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteInfraestruturaById(int id)
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

        public async Task<bool> UpdateInfraestrutura(Infraestrutura Path)
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
