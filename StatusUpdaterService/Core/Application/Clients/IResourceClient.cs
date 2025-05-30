using StatusUpdaterService.Core.Domain.Models;

namespace StatusUpdaterService.Core.Application.Clients
{
    public interface IResourceClient
    {
        Task<List<Resource>> GetAllAsync();
        Task UpdateStatusAsync(Guid bookingId, string newStatus);
    }
}
