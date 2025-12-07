using Microsoft.Extensions.Logging;
using Shuttle.Core.Pipelines;

namespace Shuttle.Hopper.Logging;

public class StartupPipelineObserver(ILogger<StartupPipelineLogger> logger, IServiceBusLoggingConfiguration serviceBusLoggingConfiguration)
    : PipelineObserver<StartupPipelineLogger>(logger, serviceBusLoggingConfiguration),
        IPipelineObserver<OnStarting>,
        IPipelineObserver<OnCreatePhysicalTransports>,
        IPipelineObserver<OnAfterCreatePhysicalTransports>,
        IPipelineObserver<OnConfigureThreadPools>,
        IPipelineObserver<OnAfterConfigureThreadPools>,
        IPipelineObserver<OnStartThreadPools>,
        IPipelineObserver<OnAfterStartThreadPools>
{
    public async Task ExecuteAsync(IPipelineContext<OnStarting> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnCreatePhysicalTransports> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnAfterCreatePhysicalTransports> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnConfigureThreadPools> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnAfterConfigureThreadPools> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnStartThreadPools> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnAfterStartThreadPools> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }
}