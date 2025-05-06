using IndoorMappingWebsite.Models;

namespace IndoorMappingWebsite.Services
{
    public interface ITipoInfraestruturaService
    {
        Task<List<TipoInfraestrutura>> GetInfraestruturasAsync();
        Task<bool> CreateInfraestruturaAsync(TipoInfraestruturaSend path);
        Task<TipoInfraestrutura> GetInfraestruturaByIdAsync(int id);
        Task<bool> DeleteInfraestruturaById(int id);
        Task<bool> UpdateInfraestrutura(TipoInfraestrutura Path);
    }
    public class TipoInfraestruturaService : ITipoInfraestruturaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "/api/TipoInfraestrutura";

        public TipoInfraestruturaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CreateInfraestruturaAsync(TipoInfraestruturaSend path)
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

        public async Task<TipoInfraestrutura> GetInfraestruturaByIdAsync(int id)
        {

            try
            {
                return await _httpClient.GetFromJsonAsync<TipoInfraestrutura>($"{_baseUrl}{id}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<TipoInfraestrutura>> GetInfraestruturasAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TipoInfraestrutura>>(_baseUrl);
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

        public async Task<bool> UpdateInfraestrutura(TipoInfraestrutura Path)
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
