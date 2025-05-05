using IndoorMappingWebsite.Models;

namespace IndoorMappingWebsite.Services
{

    public interface ILogService
    {
        Task<List<Logs>> GetLogsAsync();
        Task<bool> CreateLogAsync(LogSend log);
        Task<Logs> GetLogByIdAsync(int id);
        Task<bool> DeleteLogAsync(int id);

    }
    public class LogService : ILogService
    {

        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "/api/Logs";

        public LogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Logs>> GetLogsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Logs>>(_baseUrl);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> CreateLogAsync(LogSend log)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, log);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<Logs> GetLogByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Logs>($"{_baseUrl}/{id}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteLogAsync(int id)
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
    }
}