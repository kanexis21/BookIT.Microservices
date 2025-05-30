using Microsoft.EntityFrameworkCore;
using ResourceService.Api.Core.Domain.Model;

namespace ResourceService.Api.Application.Interfaces
{
    public interface IResourceDbContext
    {
        public DbSet<Resource> Resources { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
