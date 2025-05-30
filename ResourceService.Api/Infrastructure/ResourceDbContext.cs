using Microsoft.EntityFrameworkCore;
using ResourceService.Api.Application.Interfaces;
using ResourceService.Api.Core.Domain.Model;
using ResourceService.Api.Infrastructure.EntityTypeConfiguration;

namespace ResourceService.Api.Infrastructure
{
    public class ResourceDbContext : DbContext, IResourceDbContext
    {
        public DbSet<Resource> Resources { get; set; }
        public ResourceDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ResourceConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
