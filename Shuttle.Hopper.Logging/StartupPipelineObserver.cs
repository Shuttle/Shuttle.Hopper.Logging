using Microsoft.Extensions.Logging;
using Shuttle.Core.Pipelines;

namespace Shuttle.Hopper.Logging;

public class StartupPipelineObserver(ILogger<StartupPipelineLogger> logger, IServiceBusLoggingConfiguration serviceBusLoggingConfiguration)
    : PipelineObserver<StartupPipelineLogger>(logger, serviceBusLoggingConfiguration),
        IPipelineObserver<Starting>,
        IPipelineObserver<CreatePhysicalTransports>,
        IPipelineObserver<PhysicalTransportsCreated>,
        IPipelineObserver<ConfigureThreadPools>,
        IPipelineObserver<ThreadPoolsConfigured>,
        IPipelineObserver<StartThreadPools>,
        IPipelineObserver<ThreadPoolsStarted>
{
    public async Task ExecuteAsync(IPipelineContext<Starting> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<CreatePhysicalTransports> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<PhysicalTransportsCreated> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<ConfigureThreadPools> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<ThreadPoolsConfigured> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<StartThreadPools> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<ThreadPoolsStarted> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }
}