using FluentValidation;
using MediatR;
using SupportService.Core.Application.Common;
using SupportService.Core.Application.Common.Services;
using SupportService.Core.Application.Services.Clients;
using System.Reflection;

namespace SupportService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<AccessTokenHandler>();

            services.AddHttpClient<IUserClient, UserClient>("IdentityServer", client =>
            {
                client.BaseAddress = new Uri("https://localhost:5005");
            }).AddHttpMessageHandler<AccessTokenHandler>();

            services.AddSingleton<IUserClient,UserClient>();


            services.AddHttpClient("ApiGateway", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7099");
            }).AddHttpMessageHandler<AccessTokenHandler>();

            services.AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            return services;
        }

    }
}
