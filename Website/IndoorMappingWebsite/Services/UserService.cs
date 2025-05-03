using IndoorMappingWebsite.Components.Pages;
using IndoorMappingWebsite.Models;

namespace IndoorMappingWebsite.Services
{
    
    public interface IUserService
    {
        Task<List<UserTable.UserGet>> GetUsersAsync();
        Task<bool> CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task<bool> DeleteUserById(int id);
        Task<bool> UpdateUserById(UserTable.UserEdit user);
    }

    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "/api/Auth/register2";
        private readonly string _baseUrlUsuarios = "/api/Usuarios";

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserTable.UserGet>> GetUsersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<UserTable.UserGet>>(_baseUrlUsuarios);
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

        public async Task<bool> DeleteUserById(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrlUsuarios}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateUserById(UserTable.UserEdit user)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrlUsuarios}/{user.usuarioId}", user);
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
