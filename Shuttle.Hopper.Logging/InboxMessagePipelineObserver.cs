using Microsoft.Extensions.Logging;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Hopper.Logging;

public class InboxMessagePipelineObserver(ILogger<InboxMessagePipelineLogger> logger, IServiceBusLoggingConfiguration serviceBusLoggingConfiguration)
    : PipelineObserver<InboxMessagePipelineLogger>(logger, serviceBusLoggingConfiguration),
        IPipelineObserver<ReceiveMessage>,
        IPipelineObserver<MessageReceived>,
        IPipelineObserver<DeserializeTransportMessage>,
        IPipelineObserver<TransportMessageDeserialized>,
        IPipelineObserver<DecompressMessage>,
        IPipelineObserver<MessageDecompressed>,
        IPipelineObserver<DecryptMessage>,
        IPipelineObserver<MessageDecrypted>,
        IPipelineObserver<DeserializeMessage>,
        IPipelineObserver<MessageDeserialized>,
        IPipelineObserver<HandleMessage>,
        IPipelineObserver<MessageHandled>,
        IPipelineObserver<AcknowledgeMessage>,
        IPipelineObserver<MessageAcknowledged>
{
    public async Task ExecuteAsync(IPipelineContext<AcknowledgeMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<MessageAcknowledged> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<MessageDecompressed> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<MessageDecrypted> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<MessageDeserialized> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<TransportMessageDeserialized> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<MessageReceived> pipelineContext, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(pipelineContext);

        await TraceAsync(pipelineContext, $"working = {pipelineContext.Pipeline.State.GetWorking()} / has message = {pipelineContext.Pipeline.State.GetReceivedMessage() != null}");
    }

    public async Task ExecuteAsync(IPipelineContext<MessageHandled> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<DecompressMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<DecryptMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<DeserializeMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<DeserializeTransportMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<ReceiveMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(Guard.AgainstNull(pipelineContext), $"working = {pipelineContext.Pipeline.State.GetWorking()} / has message = {pipelineContext.Pipeline.State.GetReceivedMessage() != null}");
    }

    public async Task ExecuteAsync(IPipelineContext<HandleMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }
}