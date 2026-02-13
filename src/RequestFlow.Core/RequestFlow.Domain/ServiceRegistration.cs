using Microsoft.Extensions.DependencyInjection;

namespace RequestFlow.Domain;

public static class ServiceRegistration
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        // TODO: Register domain services when needed
        // services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        return services;
    }
}
