using Microsoft.Extensions.Logging;
using Shuttle.Core.Pipelines;

namespace Shuttle.Hopper.Logging;

public class DispatchTransportMessagePipelineObserver(ILogger<DispatchTransportMessagePipelineLogger> logger, IServiceBusLoggingConfiguration serviceBusLoggingConfiguration)
    : PipelineObserver<DispatchTransportMessagePipelineLogger>(logger, serviceBusLoggingConfiguration),
        IPipelineObserver<FindMessageRoute>,
        IPipelineObserver<MessageRouteFound>,
        IPipelineObserver<SerializeTransportMessage>,
        IPipelineObserver<TransportMessageSerialized>,
        IPipelineObserver<DispatchTransportMessage>,
        IPipelineObserver<TransportMessageDispatched>
{
    public async Task ExecuteAsync(IPipelineContext<FindMessageRoute> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<MessageRouteFound> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<SerializeTransportMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<TransportMessageSerialized> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<DispatchTransportMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<TransportMessageDispatched> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }
}