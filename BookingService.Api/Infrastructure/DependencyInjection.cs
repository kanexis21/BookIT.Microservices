using BookingService.Api.Core.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Api.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistense(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<BookingDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IBookingDbContext>(provider => provider.GetService<BookingDbContext>());


            return services;
        }
    }
}
