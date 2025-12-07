namespace Shuttle.Hopper.Logging;

public interface IServiceBusLoggingConfiguration
{
    bool ShouldLogPipelineEventType(Type pipelineEventType);
    bool ShouldLogPipelineType(Type pipelineType);
}