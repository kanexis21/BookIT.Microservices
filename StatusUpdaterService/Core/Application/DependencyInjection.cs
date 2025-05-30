using StatusUpdaterService.Core.Application.Clients;
using StatusUpdaterService.Core.Application.Services;

namespace StatusUpdaterService.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient<TokenService>();
            services.AddTransient<AccessTokenHandler>();

            services.AddHttpClient<IBookingClient, BookingClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7187");
            }).AddHttpMessageHandler<AccessTokenHandler>();
            services.AddHttpClient<IRoomClient, RoomClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7231");
            }).AddHttpMessageHandler<AccessTokenHandler>();

            services.AddHttpClient<IResourceClient, ResourceClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7166");
            }).AddHttpMessageHandler<AccessTokenHandler>();

            // 👇 Обновление статусов
            services.AddScoped<IStatusUpdateService, StatusUpdateService>();
            services.AddHostedService<StatusUpdateHostedService>();

            return services;
        }
    }
}
