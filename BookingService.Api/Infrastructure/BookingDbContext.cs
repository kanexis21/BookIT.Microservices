using BookingService.Api.Core.Application.Interfaces;
using BookingService.Api.Core.Domain.Model;
using BookingService.Api.Infrastructure.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Api.Infrastructure
{
    public class BookingDbContext : DbContext, IBookingDbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public BookingDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BookingConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
