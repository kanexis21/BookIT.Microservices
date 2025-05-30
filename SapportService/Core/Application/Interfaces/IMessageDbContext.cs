using Microsoft.EntityFrameworkCore;
using SapportService.Core.Domain.Models;

namespace SupportService.Core.Application.Interfaces
{
    public interface IMessageDbContext
    {
        public DbSet<Message> Messages { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
