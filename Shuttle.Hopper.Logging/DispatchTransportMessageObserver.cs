using Microsoft.Extensions.Logging;
using Shuttle.Core.Pipelines;

namespace Shuttle.Hopper.Logging;

public class DispatchTransportMessagePipelineObserver(ILogger<DispatchTransportMessagePipelineLogger> logger, IServiceBusLoggingConfiguration serviceBusLoggingConfiguration)
    : PipelineObserver<DispatchTransportMessagePipelineLogger>(logger, serviceBusLoggingConfiguration),
        IPipelineObserver<OnFindRouteForMessage>,
        IPipelineObserver<OnAfterFindRouteForMessage>,
        IPipelineObserver<OnSerializeTransportMessage>,
        IPipelineObserver<OnAfterSerializeTransportMessage>,
        IPipelineObserver<OnDispatchTransportMessage>,
        IPipelineObserver<OnAfterDispatchTransportMessage>
{
    public async Task ExecuteAsync(IPipelineContext<OnFindRouteForMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnAfterFindRouteForMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnSerializeTransportMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnAfterSerializeTransportMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnDispatchTransportMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnAfterDispatchTransportMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }
}