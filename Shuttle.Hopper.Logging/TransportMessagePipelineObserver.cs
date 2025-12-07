using Microsoft.Extensions.Logging;
using Shuttle.Core.Pipelines;

namespace Shuttle.Hopper.Logging;

public class TransportMessagePipelineObserver(ILogger<TransportMessagePipelineLogger> logger, IServiceBusLoggingConfiguration serviceBusLoggingConfiguration)
    : PipelineObserver<TransportMessagePipelineLogger>(logger, serviceBusLoggingConfiguration),
        IPipelineObserver<OnAssembleMessage>,
        IPipelineObserver<OnAfterAssembleMessage>,
        IPipelineObserver<OnSerializeMessage>,
        IPipelineObserver<OnAfterSerializeMessage>,
        IPipelineObserver<OnEncryptMessage>,
        IPipelineObserver<OnAfterEncryptMessage>,
        IPipelineObserver<OnCompressMessage>,
        IPipelineObserver<OnAfterCompressMessage>
{
    public async Task ExecuteAsync(IPipelineContext<OnAssembleMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnAfterAssembleMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnSerializeMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnAfterSerializeMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnEncryptMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnAfterEncryptMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnCompressMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<OnAfterCompressMessage> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }
}