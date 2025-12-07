using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;

namespace Shuttle.Hopper.Logging;

public class TransportEventLogger : IHostedService
{
    private readonly ServiceBusOptions _serviceBusOptions;
    private readonly ILogger<TransportEventLogger> _logger;
    private readonly ServiceBusLoggingOptions _serviceBusLoggingOptions;

    public TransportEventLogger(ILogger<TransportEventLogger> logger, IOptions<ServiceBusOptions> serviceBusOptions, IOptions<ServiceBusLoggingOptions> serviceBusLoggingOptions)
    {
        _serviceBusOptions = Guard.AgainstNull(Guard.AgainstNull(serviceBusOptions).Value);
        _serviceBusLoggingOptions = Guard.AgainstNull(Guard.AgainstNull(serviceBusLoggingOptions).Value);
        _logger = Guard.AgainstNull(logger);

        if (!_serviceBusLoggingOptions.QueueEvents)
        {
            return;
        }

        _serviceBusOptions.TransportCreated += OnTransportCreated;
        _serviceBusOptions.TransportDisposing += OnTransportDisposing;
        _serviceBusOptions.TransportDisposed += OnTransportDisposed;
        _serviceBusOptions.MessageAcknowledged += OnMessageAcknowledged;
        _serviceBusOptions.MessageSent += OnMessageSent;
        _serviceBusOptions.MessageReceived += OnMessageReceived;
        _serviceBusOptions.MessageReleased += OnMessageReleased;
        _serviceBusOptions.TransportOperation += OnTransportOperation;
    }

    private Task OnTransportOperation(TransportOperationEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[{Scheme}.Operation] : transport name = '{TransportName}' / operation = '{Operation}' / managed thread id = {CurrentManagedThreadId}", eventArgs.Transport.Uri.Uri.Scheme, eventArgs.Transport.Uri.TransportName, eventArgs.Operation, Environment.CurrentManagedThreadId);

        return Task.CompletedTask;
    }

    private Task OnMessageReleased(MessageReleasedEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[{Scheme}.MessageReleased] : transport name = '{TransportName}' / managed thread id = {CurrentManagedThreadId}", eventArgs.Transport.Uri.Uri.Scheme, eventArgs.Transport.Uri.TransportName, Environment.CurrentManagedThreadId);
        
        return Task.CompletedTask;
    }

    private Task OnMessageReceived(MessageReceivedEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[{Scheme}.MessageReceived] : transport name = '{TransportName}' / managed thread id = {CurrentManagedThreadId}", eventArgs.Transport.Uri.Uri.Scheme, eventArgs.Transport.Uri.TransportName, Environment.CurrentManagedThreadId);

            return Task.CompletedTask;
    }

    private Task OnMessageSent(MessageSentEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[{Scheme}.MessageEnqueued] : transport name = '{TransportName}' / message type = '{MessageType}' / message id = '{MessageId}' / managed thread id = {CurrentManagedThreadId}", eventArgs.Transport.Uri.Uri.Scheme, eventArgs.Transport.Uri.TransportName, eventArgs.TransportMessage.MessageType, eventArgs.TransportMessage.MessageId, Environment.CurrentManagedThreadId);

        return Task.CompletedTask;
    }

    private Task OnMessageAcknowledged(MessageAcknowledgedEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[{Scheme}.MessageAcknowledged] : transport name = '{TransportName}' / managed thread id = {CurrentManagedThreadId}", eventArgs.Transport.Uri.Uri.Scheme, eventArgs.Transport.Uri.TransportName, Environment.CurrentManagedThreadId);

        return Task.CompletedTask;
    }

    private Task OnTransportDisposed(TransportEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace($"[OnTransportDisposed] : uri = '{eventArgs.Transport.Uri}'");

        return Task.CompletedTask;
    }

    private Task OnTransportDisposing(TransportEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace($"[OnTransportDisposing] : uri = '{eventArgs.Transport.Uri}'");

        return Task.CompletedTask;
    }

    private Task OnTransportCreated(TransportEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace($"[OnTransportCreated] : uri = '{eventArgs.Transport.Uri}'");

        return Task.CompletedTask;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_serviceBusLoggingOptions.QueueEvents)
        {
            _serviceBusOptions.TransportCreated -= OnTransportCreated;
            _serviceBusOptions.TransportDisposing -= OnTransportDisposing;
            _serviceBusOptions.TransportDisposed -= OnTransportDisposed;
            _serviceBusOptions.MessageAcknowledged -= OnMessageAcknowledged;
            _serviceBusOptions.MessageSent -= OnMessageSent;
            _serviceBusOptions.MessageReceived -= OnMessageReceived;
            _serviceBusOptions.MessageReleased -= OnMessageReleased;
            _serviceBusOptions.TransportOperation -= OnTransportOperation;
        }

        await Task.CompletedTask;
    }
}