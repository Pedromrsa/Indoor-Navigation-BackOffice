
using IndoorMappingWebsite.Components.Pages;
using IndoorMappingWebsite.Models;

namespace IndoorMappingWebsite.Services
{
    public interface IUserLocService
    {
        Task<List<UserLoc>> GetUsersAsync();
        List<UserLoc> GetUsersLastPosition(List<UserLoc> locs);
    }
    public class UserLocService : IUserLocService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "/api/LocalizacoesUsuario";

        public UserLocService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserLoc>> GetUsersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<UserLoc>>(_baseUrl);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public List<UserLoc> GetUsersLastPosition(List<UserLoc> locs) {
            return locs
                    .GroupBy(l => l.usuarioId)
                    .Select(g => g.OrderByDescending(l => l.dataHora).First())
                    .ToList();
        }
    }
}
