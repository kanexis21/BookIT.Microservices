using Microsoft.EntityFrameworkCore;
using RoomService.Api.Core.Application.Interfaces;
using RoomService.Api.Core.Domain.Model;
using RoomService.Api.Infrastructure.EntityTypeConfiguration;

namespace RoomService.Api.Infrastructure
{
    public class RoomDbContext : DbContext, IRoomDbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomPhoto> RoomPhotos { get; set; }
        public RoomDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoomConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
