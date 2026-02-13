using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RequestFlow.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: Add DbContext when implemented
        // services.AddDbContext<RequestFlowDbContext>(options =>
        //     options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // TODO: Register repositories
        // services.AddScoped<IRequestRepository, RequestRepository>();

        return services;
    }
}
