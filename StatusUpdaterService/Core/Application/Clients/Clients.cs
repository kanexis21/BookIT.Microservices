using Newtonsoft.Json;
using StatusUpdaterService.Core.Domain.Models;
using System.Net.Http;
using System.Text;

namespace StatusUpdaterService.Core.Application.Clients
{
    class BookingClient : IBookingClient
    {
        private readonly HttpClient _httpClient;
        public BookingClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Booking>> GetAllAsync()
        {
            var bookingsResponse = await _httpClient.GetAsync($"/api/booking");
            if (!bookingsResponse.IsSuccessStatusCode)
                return new();

            var bookingsJson = await bookingsResponse.Content.ReadAsStringAsync();
            var bookings = JsonConvert.DeserializeObject<List<Booking>>(bookingsJson);

            return bookings;
        }

        public async Task UpdateStatusAsync(Guid bookingId, string newStatus)
        {
            var requestBody = new
            {
                BookingId = bookingId,
                Status = newStatus
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/booking/status", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Ошибка обновления статуса ресурса: {response.StatusCode}");
            }
        }
    }
    class ResourceClient : IResourceClient
    {
        private readonly HttpClient _httpClient;
        public ResourceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Resource>> GetAllAsync()
        {
            var resourcesResponse = await _httpClient.GetAsync($"/api/resource");
            if (!resourcesResponse.IsSuccessStatusCode)
                return new();

            var resourcesJson = await resourcesResponse.Content.ReadAsStringAsync();
            var resources = JsonConvert.DeserializeObject<List<Resource>>(resourcesJson);

            return resources;
        }

        public async Task UpdateStatusAsync(Guid resourceId, string newStatus)
        {
            var requestBody = new
            {
                Status = 0
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PatchAsync($"/api/resource/{resourceId}/status", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Ошибка обновления статуса ресурса: {response.StatusCode}");
            }
        }
    }
    class RoomClient : IRoomClient
    {
        private readonly HttpClient _httpClient;
        public RoomClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Room>> GetAllAsync()
        {
            var roomResponse = await _httpClient.GetAsync($"/api/room");
            if (!roomResponse.IsSuccessStatusCode)
                return new();

            var roomsJson = await roomResponse.Content.ReadAsStringAsync();
            var rooms = JsonConvert.DeserializeObject<List<Room>>(roomsJson);

            return rooms;
        }

        public async Task UpdateStatusAsync(Guid roomId, string newStatus)
        {
            var requestBody = new
            {
                Status = 0
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PatchAsync($"/api/room/{roomId}/status", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Ошибка обновления статуса ресурса: {response.StatusCode}");
            }
        }
    }
}
