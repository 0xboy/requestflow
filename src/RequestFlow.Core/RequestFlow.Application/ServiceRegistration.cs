using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RequestFlow.Domain;

namespace RequestFlow.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDomain();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly);
        });

        services.AddAutoMapper(typeof(ServiceRegistration).Assembly);

        return services;
    }
}
