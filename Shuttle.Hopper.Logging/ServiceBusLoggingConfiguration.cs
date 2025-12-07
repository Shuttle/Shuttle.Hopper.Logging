using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;

namespace Shuttle.Hopper.Logging;

public class ServiceBusLoggingConfiguration : IServiceBusLoggingConfiguration
{
    private readonly List<Type> _pipelineEventTypes = [];
    private readonly List<Type> _pipelineTypes = [];

    public ServiceBusLoggingConfiguration(IOptions<ServiceBusLoggingOptions> serviceBusLoggingOptions, ILogger<ServiceBusLoggingConfiguration> logger)
    {
        Guard.AgainstNull(Guard.AgainstNull(serviceBusLoggingOptions).Value);
        Guard.AgainstNull(logger);

        foreach (var pipelineType in serviceBusLoggingOptions.Value.PipelineTypes)
        {
            try
            {
                _pipelineTypes.Add(Guard.AgainstNull(Type.GetType(pipelineType)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        foreach (var pipelineEventType in serviceBusLoggingOptions.Value.PipelineEventTypes)
        {
            try
            {
                _pipelineEventTypes.Add(Guard.AgainstNull(Type.GetType(pipelineEventType)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }

    public bool ShouldLogPipelineType(Type pipelineType)
    {
        return !_pipelineTypes.Any() || _pipelineTypes.Contains(Guard.AgainstNull(pipelineType));
    }

    public bool ShouldLogPipelineEventType(Type pipelineEventType)
    {
        return !_pipelineEventTypes.Any() || _pipelineEventTypes.Contains(Guard.AgainstNull(pipelineEventType));
    }
}