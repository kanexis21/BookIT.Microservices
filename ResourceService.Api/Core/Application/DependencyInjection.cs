using FluentValidation;
using MediatR;
using System.Reflection;
using ResourceService.Api.Application.Common;

namespace ResourceService.Api.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            services.AddHttpClient("ResourceService", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7166");
            });
            return services;
        }

    }
}
