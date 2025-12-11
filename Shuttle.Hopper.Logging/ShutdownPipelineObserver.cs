using Microsoft.Extensions.Logging;
using Shuttle.Core.Pipelines;

namespace Shuttle.Hopper.Logging;

public class ShutdownPipelineObserver(ILogger<ShutdownPipelineLogger> logger, IServiceBusLoggingConfiguration serviceBusLoggingConfiguration)
    : PipelineObserver<ShutdownPipelineLogger>(logger, serviceBusLoggingConfiguration),
        IPipelineObserver<Stopping>,
        IPipelineObserver<Stopped>
{
    public async Task ExecuteAsync(IPipelineContext<Stopping> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<Stopped> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }
}