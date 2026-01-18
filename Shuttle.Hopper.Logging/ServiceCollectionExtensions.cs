using Microsoft.Extensions.DependencyInjection;

namespace Shuttle.Hopper.Logging;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddHopperLogging()
        {
            services.AddHostedService<TransportEventLogger>();

            return services;
        }
    }
}