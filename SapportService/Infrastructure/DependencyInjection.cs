using Microsoft.EntityFrameworkCore;
using SupportService.Core.Application.Interfaces;
using SupportService.Infrastructure;

namespace SupportService.Api.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistense(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<MessageDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IMessageDbContext>(provider => provider.GetService<MessageDbContext>());

            return services;
        }
    }
}
