using System.Net.Http;
using System.Text.Json;

namespace SupportService.Core.Application.Services.Clients
{
    public class UserClient : IUserClient
    {
        private readonly HttpClient _httpClient;

        public UserClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetUserNameAsync(Guid userId)
        {
            var response = await _httpClient.GetAsync($"api/user/profile/{userId}");
            if (!response.IsSuccessStatusCode)
                return "Неизвестный";

            var content = await response.Content.ReadAsStringAsync();

            try
            {
                using var doc = JsonDocument.Parse(content);
                var root = doc.RootElement;

                if (root.TryGetProperty("firstName", out var firstName))
                {
                    return firstName.GetString();
                }

                return "Неизвестный";
            }
            catch
            {
                return "Неизвестный";
            }
        }

    }

}
