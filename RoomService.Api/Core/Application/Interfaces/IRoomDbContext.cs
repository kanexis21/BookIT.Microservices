using Microsoft.EntityFrameworkCore;
using RoomService.Api.Core.Domain.Model;

namespace RoomService.Api.Core.Application.Interfaces
{
    public interface IRoomDbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomPhoto> RoomPhotos { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
