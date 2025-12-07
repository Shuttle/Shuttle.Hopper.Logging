using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Hopper.Logging;

public class ServiceBusLoggingBuilder(IServiceCollection services)
{
    public ServiceBusLoggingOptions Options
    {
        get;
        set => field = value ?? throw new ArgumentNullException(nameof(value));
    } = new();

    public IServiceCollection Services { get; } = Guard.AgainstNull(services);
}