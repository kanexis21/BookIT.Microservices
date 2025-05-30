using Microsoft.EntityFrameworkCore;
using SapportService.Core.Domain.Models;
using SupportService.Core.Application.Interfaces;
using SupportService.Infrastructure.EntityTypeConfiguration;

namespace SupportService.Infrastructure
{
    public class MessageDbContext : DbContext, IMessageDbContext
    {
        public DbSet<Message> Messages { get; set; }
        public MessageDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MessageConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
