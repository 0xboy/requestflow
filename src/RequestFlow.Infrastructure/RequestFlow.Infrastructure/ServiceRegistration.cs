using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RequestFlow.Application.Interfaces.Services;
using RequestFlow.Infrastructure.Services;

namespace RequestFlow.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRequestService, RequestService>();

        return services;
    }
}
