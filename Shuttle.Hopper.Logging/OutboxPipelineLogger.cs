using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Hopper.Logging;

public class OutboxPipelineLogger : IHostedService
{
    private readonly PipelineOptions _pipelineOptions;
    private readonly ILogger<OutboxPipelineLogger> _logger;
    private readonly Type _pipelineType = typeof(OutboxPipeline);
    private readonly IServiceBusLoggingConfiguration _serviceBusLoggingConfiguration;

    public OutboxPipelineLogger(ILogger<OutboxPipelineLogger> logger, IOptions<PipelineOptions> pipelineOptions, IServiceBusLoggingConfiguration serviceBusLoggingConfiguration)
    {
        _logger = Guard.AgainstNull(logger);
        _pipelineOptions = Guard.AgainstNull(Guard.AgainstNull(pipelineOptions).Value);
        _serviceBusLoggingConfiguration = Guard.AgainstNull(serviceBusLoggingConfiguration);

        if (_serviceBusLoggingConfiguration.ShouldLogPipelineType(_pipelineType))
        {
            _pipelineOptions.PipelineCreated += OnPipelineCreated;
        }
    }

    private Task OnPipelineCreated(PipelineEventArgs eventArgs, CancellationToken cancellationToken)
    {
        if (eventArgs.Pipeline.GetType() == _pipelineType)
        {
            eventArgs.Pipeline.AddObserver(new OutboxPipelineObserver(_logger, _serviceBusLoggingConfiguration));
        }

        return Task.CompletedTask;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_serviceBusLoggingConfiguration.ShouldLogPipelineType(_pipelineType))
        {
            _pipelineOptions.PipelineCreated -= OnPipelineCreated;
        }

        await Task.CompletedTask;
    }
}