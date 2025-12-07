using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;
using Shuttle.Core.Pipelines;

namespace Shuttle.Hopper.Logging;

public class TransportMessageDeferredLogger : IHostedService
{
    private readonly ServiceBusOptions _serviceBusOptions;
    private readonly ILogger<TransportMessageDeferredLogger> _logger;
    private readonly ServiceBusLoggingOptions _serviceBusLoggingOptions;

    public TransportMessageDeferredLogger(ILogger<TransportMessageDeferredLogger> logger, IOptions<ServiceBusOptions> serviceBusOptions, IOptions<ServiceBusLoggingOptions> serviceBusLoggingOptions)
    {
        _serviceBusOptions = Guard.AgainstNull(Guard.AgainstNull(serviceBusOptions).Value);
        _serviceBusLoggingOptions = Guard.AgainstNull(Guard.AgainstNull(serviceBusLoggingOptions).Value);
        _logger = Guard.AgainstNull(logger);

        if (_serviceBusLoggingOptions.TransportMessageDeferred)
        {
            _serviceBusOptions.TransportMessageDeferred += OnTransportMessageDeferred;
        }
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_serviceBusLoggingOptions.TransportMessageDeferred)
        {
            _serviceBusOptions.TransportMessageDeferred -= OnTransportMessageDeferred;
        }

        await Task.CompletedTask;
    }

    private Task OnTransportMessageDeferred(TransportMessageDeferredEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[TransportMessageDeferred] : message id = '{MessageId}' / message type = '{MessageType}' / managed thread id = {CurrentManagedThreadId}", eventArgs.TransportMessage.MessageId, eventArgs.TransportMessage.MessageType, Environment.CurrentManagedThreadId);

        return Task.CompletedTask;
    }
}