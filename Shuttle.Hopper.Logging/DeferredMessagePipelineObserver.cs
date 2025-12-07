using Microsoft.Extensions.Logging;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Hopper.Logging;

public class DeferredMessagePipelineObserver(ILogger<DeferredMessagePipelineLogger> logger, IServiceBusLoggingConfiguration serviceBusLoggingConfiguration)
    : PipelineObserver<DeferredMessagePipelineLogger>(logger, serviceBusLoggingConfiguration),
        IPipelineObserver<OnGetMessage>,
        IPipelineObserver<OnAfterGetMessage>,
        IPipelineObserver<OnDeserializeTransportMessage>,
        IPipelineObserver<OnAfterDeserializeTransportMessage>,
        IPipelineObserver<OnProcessDeferredMessage>,
        IPipelineObserver<OnAfterProcessDeferredMessage>
{
    public async Task ExecuteAsync(IPipelineContext<OnAfterDeserializeTransportMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnAfterGetMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnDeserializeTransportMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnGetMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(pipelineContext);

        await TraceAsync(pipelineContext, $"working = {pipelineContext.Pipeline.State.GetWorking()} / has message = {pipelineContext.Pipeline.State.GetReceivedMessage() != null}");
    }

    public async Task ExecuteAsync(IPipelineContext<OnProcessDeferredMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnAfterProcessDeferredMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }
}