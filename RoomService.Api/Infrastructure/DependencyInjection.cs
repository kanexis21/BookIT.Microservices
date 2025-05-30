using Microsoft.EntityFrameworkCore;
using RoomService.Api.Core.Application.Interfaces;

namespace RoomService.Api.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistense(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<RoomDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IRoomDbContext>(provider => provider.GetService<RoomDbContext>());

            return services;
        }
    }
}
