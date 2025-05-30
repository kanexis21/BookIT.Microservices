using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RoomService.Api.Core.Domain.Model;

namespace RoomService.Api.Infrastructure.EntityTypeConfiguration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
            builder.HasMany(r => r.Photos).WithOne(p => p.Room).HasForeignKey(p => p.RoomId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(r => r.Capacity).IsRequired();
            builder.Property(r => r.Location).HasMaxLength(200);

            builder.ToTable("Помещения");
        }
    }
}
