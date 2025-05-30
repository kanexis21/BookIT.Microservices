using StatusUpdaterService.Core.Domain.Models;

namespace StatusUpdaterService.Core.Application.Clients
{
    public interface IRoomClient
    {
        Task<List<Room>> GetAllAsync();
        Task UpdateStatusAsync(Guid roomId, string newStatus);
    }
}
