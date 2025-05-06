using IndoorMappingWebsite.Models;
using System.Net.Http;

namespace IndoorMappingWebsite.Services
{

    public interface ILoginService
    {
        Task<string?> LoginAsync(string email, string password);
        Task LogoutAsync();

    }
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        

        public async Task<string?> LoginAsync(String email, String password)
        {
            var loginData = new LoginRequest { email = email, password = password };
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", loginData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                return result?["token"];
            }

            return null;
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
