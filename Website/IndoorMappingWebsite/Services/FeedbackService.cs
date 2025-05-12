using IndoorMappingWebsite.Models;

namespace IndoorMappingWebsite.Services
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> GetFeedbacksAsync();
        Task<bool> DeleteFeedbackById(int id);
        Task<List<FeedbackUserGet>> GetFeedbacksForUser();
        Task<bool> UpdateFeedbackForUser(FeedbackUserGet Path);
        Task<bool> CreateFeedbackForUser(FeedbackUser Path);
        Task<bool> DeleteFeedbacForUserkById(int id);
    }
    public class FeedbackService : IFeedbackService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "/api/FeedbackCaminhos";
        private readonly string _baseUrlFeedbackUser = "/api/FeedbackForUser";

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

        public async Task<List<FeedbackUserGet>> GetFeedbacksForUser()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<FeedbackUserGet>>(_baseUrlFeedbackUser);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }
        public async Task<bool> UpdateFeedbackForUser(FeedbackUserGet Path)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrlFeedbackUser}/{Path.id}", Path);
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

        public async Task<bool> DeleteFeedbacForUserkById(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrlFeedbackUser}/{id}");
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
        public async Task<bool> CreateFeedbackForUser(FeedbackUser feedback)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(_baseUrlFeedbackUser, feedback);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"API call error: {ex.Message}");
                throw;
            }
        }
    }
}
