using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SapportService.Core.Domain.Models;

namespace SupportService.Infrastructure.EntityTypeConfiguration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Text).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.SenderName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ReceiverId).IsRequired();
            builder.ToTable("Сообщения");
        }
    }
}
