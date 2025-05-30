using StatusUpdaterService.Core.Application.Clients;

namespace StatusUpdaterService.Core.Application.Services
{
    public class StatusUpdateService : IStatusUpdateService
    {
        private readonly IBookingClient _bookingClient;
        private readonly IResourceClient _resourceClient;
        private readonly IRoomClient _roomClient;

        public StatusUpdateService(IBookingClient bookingClient, IResourceClient resourceClient, IRoomClient roomClient)
        {
            _bookingClient = bookingClient;
            _resourceClient = resourceClient;
            _roomClient = roomClient;
        }

        public async Task UpdateStatusesAsync()
        {
            var bookings = await _bookingClient.GetAllAsync();
            var resources = await _resourceClient.GetAllAsync();
            var rooms = await _roomClient.GetAllAsync();
            foreach (var booking in bookings)
            {
                if (booking.EndTime < DateTime.Now && booking.Status == "0")
                {
                    foreach (var room in rooms)
                    {
                        if (room.Status != "1" && booking.RoomId == room.Id)
                        {
                            await _resourceClient.UpdateStatusAsync(room.Id, "1");

                        }
                    }
                    foreach (var resource in resources)
                    {
                        if (resource.Status != "Доступен" && booking.ResourceId == resource.Id)
                        {
                            await _resourceClient.UpdateStatusAsync(resource.Id, "Доступен");
                            
                        }
                    }
                    await _bookingClient.UpdateStatusAsync(booking.Id, "2");
                }
            }
        }
    }

}
