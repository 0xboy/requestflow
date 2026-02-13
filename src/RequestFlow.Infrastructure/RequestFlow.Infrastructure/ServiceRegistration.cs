using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RequestFlow.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: Register external services (e.g. email, cache, file storage)
        // services.AddScoped<IEmailService, SmtpEmailService>();

        return services;
    }
}
