using BookingService.Api.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Api.Core.Application.Interfaces
{
    public interface IBookingDbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
