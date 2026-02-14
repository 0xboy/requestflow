using Microsoft.Extensions.DependencyInjection;

namespace RequestFlow.Shared;

public static class ServiceRegistration
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        // TODO: Register shared services (e.g. DateTimeProvider, validators)
        return services;
    }
}
