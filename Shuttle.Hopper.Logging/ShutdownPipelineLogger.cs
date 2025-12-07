using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Hopper.Logging;

public class ShutdownPipelineLogger : IHostedService
{
    private readonly PipelineOptions _pipelineOptions;
    private readonly ILogger<ShutdownPipelineLogger> _logger;
    private readonly Type _pipelineType = typeof(ShutdownPipeline);
    private readonly IServiceBusLoggingConfiguration _serviceBusLoggingConfiguration;

    public ShutdownPipelineLogger(ILogger<ShutdownPipelineLogger> logger, IOptions<PipelineOptions> pipelineOptions, IServiceBusLoggingConfiguration serviceBusLoggingConfiguration)
    {
        _logger = Guard.AgainstNull(logger);
        _pipelineOptions = Guard.AgainstNull(Guard.AgainstNull(pipelineOptions).Value);
        _serviceBusLoggingConfiguration = Guard.AgainstNull(serviceBusLoggingConfiguration);

        if (_serviceBusLoggingConfiguration.ShouldLogPipelineType(_pipelineType))
        {
            _pipelineOptions.PipelineCreated += OnPipelineCreated;
        }
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

    private Task OnPipelineCreated(PipelineEventArgs eventArgs, CancellationToken cancellationToken)
    {
        if (eventArgs.Pipeline.GetType() == _pipelineType)
        {
            eventArgs.Pipeline.AddObserver(new ShutdownPipelineObserver(_logger, _serviceBusLoggingConfiguration));
        }

        return Task.CompletedTask;
    }
}