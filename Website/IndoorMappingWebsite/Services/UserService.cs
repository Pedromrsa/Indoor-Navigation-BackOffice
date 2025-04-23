using IndoorMappingWebsite.Models;

namespace IndoorMappingWebsite.Services
{
    
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
        Task<bool> CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
    }

    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "/api/Auth/register2";

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<User>>(_baseUrl);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            try
            {
                Console.Write(user.nome);
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, user);
                Console.Write(response);
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Conteúdo da resposta: {content}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<User>($"{_baseUrl}{id}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }
    }
}
