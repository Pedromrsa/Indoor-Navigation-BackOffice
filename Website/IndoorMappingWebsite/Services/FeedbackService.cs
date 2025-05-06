using IndoorMappingWebsite.Models;

namespace IndoorMappingWebsite.Services
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> GetFeedbacksAsync();
        Task<bool> DeleteFeedbackById(int id);
    }
    public class FeedbackService : IFeedbackService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "/api/FeedbackCaminhos";

        public FeedbackService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Feedback>> GetFeedbacksAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Feedback>>(_baseUrl);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteFeedbackById(int id)
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

    }
}
