using Microsoft.Extensions.Logging;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Hopper.Logging;

public class OutboxPipelineObserver(ILogger<OutboxPipelineLogger> logger, IServiceBusLoggingConfiguration serviceBusLoggingConfiguration)
    : PipelineObserver<OutboxPipelineLogger>(logger, serviceBusLoggingConfiguration),
        IPipelineObserver<ReceiveMessage>,
        IPipelineObserver<MessageReceived>,
        IPipelineObserver<DeserializeTransportMessage>,
        IPipelineObserver<TransportMessageDeserialized>,
        IPipelineObserver<DispatchTransportMessage>,
        IPipelineObserver<TransportMessageDispatched>,
        IPipelineObserver<AcknowledgeMessage>,
        IPipelineObserver<MessageAcknowledged>
{
    public async Task ExecuteAsync(IPipelineContext<ReceiveMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(pipelineContext);

        await TraceAsync(pipelineContext, $"working = {pipelineContext.Pipeline.State.GetWorking()} / has message = {pipelineContext.Pipeline.State.GetReceivedMessage() != null}");
    }

    public async Task ExecuteAsync(IPipelineContext<MessageReceived> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<DeserializeTransportMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<TransportMessageDeserialized> pipelineContext, CancellationToken cancellationToken = default)
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

    public async Task ExecuteAsync(IPipelineContext<AcknowledgeMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<MessageAcknowledged> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }
}