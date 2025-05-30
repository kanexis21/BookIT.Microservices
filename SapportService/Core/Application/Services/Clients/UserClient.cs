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
            var response = await _httpClient.GetAsync($"identity/api/users/{userId}");
            if (!response.IsSuccessStatusCode)
                return "Неизвестный";

            var content = await response.Content.ReadAsStringAsync();
            return content.Trim('"');
        }
    }

}
