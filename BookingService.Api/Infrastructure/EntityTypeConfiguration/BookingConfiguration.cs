using BookingService.Api.Core.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingService.Api.Infrastructure.EntityTypeConfiguration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.StartTime)
                .IsRequired();

            builder.Property(x => x.EndTime)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(500);


            builder.HasIndex(x => new { x.ResourceId, x.StartTime, x.RoomId, x.EndTime })
                   .IsUnique();

            builder.ToTable("Бронирование");
        }
    }
}
