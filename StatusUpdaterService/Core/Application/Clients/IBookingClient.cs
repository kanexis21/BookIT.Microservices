using StatusUpdaterService.Core.Domain.Models;

namespace StatusUpdaterService.Core.Application.Clients
{
    public interface IBookingClient
    {
        Task<List<Booking>> GetAllAsync();
        Task UpdateStatusAsync(Guid bookingId, string newStatus);
    }
}
