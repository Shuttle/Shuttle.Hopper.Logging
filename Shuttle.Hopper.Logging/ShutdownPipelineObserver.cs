using Microsoft.Extensions.Logging;
using Shuttle.Core.Pipelines;

namespace Shuttle.Hopper.Logging;

public class ShutdownPipelineObserver(ILogger<ShutdownPipelineLogger> logger, IServiceBusLoggingConfiguration serviceBusLoggingConfiguration)
    : PipelineObserver<ShutdownPipelineLogger>(logger, serviceBusLoggingConfiguration),
        IPipelineObserver<OnStopping>,
        IPipelineObserver<OnStopped>
{
    public async Task ExecuteAsync(IPipelineContext<OnStopping> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnStopped> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }
}