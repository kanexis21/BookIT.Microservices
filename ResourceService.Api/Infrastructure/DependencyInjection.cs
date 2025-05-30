using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourceService.Api.Application.Interfaces;

namespace ResourceService.Api.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistense(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ResourceDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IResourceDbContext>(provider => provider.GetService<ResourceDbContext>());
            return services;
        }
    }
}
