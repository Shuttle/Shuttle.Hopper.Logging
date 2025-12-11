using Microsoft.Extensions.Logging;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;
using Shuttle.Core.Reflection;

namespace Shuttle.Hopper.Logging;

public abstract class PipelineObserver<T>(ILogger<T> logger, IServiceBusLoggingConfiguration serviceBusLoggingConfiguration)
    :
        IPipelineObserver<PipelineStarting>,
        IPipelineObserver<PipelineException>,
        IPipelineObserver<AbortPipeline>
{
    private readonly Dictionary<Type, int> _eventCounts = new();
    private readonly ILogger<T> _logger = Guard.AgainstNull(logger);
    private readonly IServiceBusLoggingConfiguration _serviceBusLoggingConfiguration = Guard.AgainstNull(serviceBusLoggingConfiguration);

    public async Task ExecuteAsync(IPipelineContext<AbortPipeline> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<PipelineStarting> pipelineContext, CancellationToken cancellationToken = default)
    {
        await TraceAsync(pipelineContext);
    }

    public async Task ExecuteAsync(IPipelineContext<PipelineException> pipelineContext, CancellationToken cancellationToken = default)
    {
        var type = pipelineContext.GetType();

        Increment(type);

        var message = $"exception = '{pipelineContext.Pipeline.Exception?.AllMessages()}'";

        _logger.LogError("[{TypeName}] : pipeline = {Pipeline}{Message} / call count = {CallCount} / managed thread id = {CurrentManagedThreadId}", type.Name, pipelineContext.Pipeline.GetType().FullName, string.IsNullOrEmpty(message) ? string.Empty : $" / {message}", _eventCounts[type], Environment.CurrentManagedThreadId);

        await Task.CompletedTask;
    }

    private void Increment(Type type)
    {
        _eventCounts.TryAdd(type, 0);
        _eventCounts[type] += 1;
    }

    protected async Task TraceAsync(IPipelineContext pipelineContext, string message = "")
    {
        var type = Guard.AgainstNull(pipelineContext).EventType;

        if (!_serviceBusLoggingConfiguration.ShouldLogPipelineEventType(type))
        {
            return;
        }

        Increment(type);

        _logger.LogTrace("[{TypeName}] : pipeline = {Pipeline}{Message} / call count = {CallCount} / managed thread id = {CurrentManagedThreadId}", type.Name, pipelineContext.Pipeline.GetType().FullName, string.IsNullOrEmpty(message) ? string.Empty : $" / {message}", _eventCounts[type], Environment.CurrentManagedThreadId);

        await Task.CompletedTask;
    }
}