using BookingService.Api.Application.Common;
using BookingService.Api.Core.Application.Common.Services;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace BookingService.Api.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddHttpContextAccessor();
            services.AddTransient<AccessTokenHandler>();
            services.AddHttpClient("ApiGateway", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7099");
            })
            .AddHttpMessageHandler<AccessTokenHandler>();
            return services;
        }

    }
}
