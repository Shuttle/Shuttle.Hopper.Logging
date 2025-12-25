using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;

namespace Shuttle.Hopper.Logging;

public class TransportEventLogger : IHostedService
{
    private readonly ILogger<TransportEventLogger> _logger;
    private readonly ServiceBusOptions _serviceBusOptions;

    public TransportEventLogger(ILogger<TransportEventLogger> logger, IOptions<ServiceBusOptions> serviceBusOptions)
    {
        _serviceBusOptions = Guard.AgainstNull(Guard.AgainstNull(serviceBusOptions).Value);
        _logger = Guard.AgainstNull(logger);

        _serviceBusOptions.TransportCreated += OnTransportCreated;
        _serviceBusOptions.TransportDisposing += OnTransportDisposing;
        _serviceBusOptions.TransportDisposed += OnTransportDisposed;
        _serviceBusOptions.MessageAcknowledged += OnMessageAcknowledged;
        _serviceBusOptions.MessageSent += OnMessageSent;
        _serviceBusOptions.MessageReceived += OnMessageReceived;
        _serviceBusOptions.MessageReleased += OnMessageReleased;
        _serviceBusOptions.TransportOperation += OnTransportOperation;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _serviceBusOptions.TransportCreated -= OnTransportCreated;
        _serviceBusOptions.TransportDisposing -= OnTransportDisposing;
        _serviceBusOptions.TransportDisposed -= OnTransportDisposed;
        _serviceBusOptions.MessageAcknowledged -= OnMessageAcknowledged;
        _serviceBusOptions.MessageSent -= OnMessageSent;
        _serviceBusOptions.MessageReceived -= OnMessageReceived;
        _serviceBusOptions.MessageReleased -= OnMessageReleased;
        _serviceBusOptions.TransportOperation -= OnTransportOperation;

        return Task.CompletedTask;
    }

    private Task OnMessageAcknowledged(MessageAcknowledgedEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[{Scheme}.MessageAcknowledged] : transport name = '{TransportName}' / managed thread id = {CurrentManagedThreadId}", eventArgs.Transport.Uri.Uri.Scheme, eventArgs.Transport.Uri.TransportName, Environment.CurrentManagedThreadId);

        return Task.CompletedTask;
    }

    private Task OnMessageReceived(MessageReceivedEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[{Scheme}.MessageReceived] : transport name = '{TransportName}' / managed thread id = {CurrentManagedThreadId}", eventArgs.Transport.Uri.Uri.Scheme, eventArgs.Transport.Uri.TransportName, Environment.CurrentManagedThreadId);

        return Task.CompletedTask;
    }

    private Task OnMessageReleased(MessageReleasedEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[{Scheme}.MessageReleased] : transport name = '{TransportName}' / managed thread id = {CurrentManagedThreadId}", eventArgs.Transport.Uri.Uri.Scheme, eventArgs.Transport.Uri.TransportName, Environment.CurrentManagedThreadId);

        return Task.CompletedTask;
    }

    private Task OnMessageSent(MessageSentEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[{Scheme}.MessageEnqueued] : transport name = '{TransportName}' / message type = '{MessageType}' / message id = '{MessageId}' / managed thread id = {CurrentManagedThreadId}", eventArgs.Transport.Uri.Uri.Scheme, eventArgs.Transport.Uri.TransportName, eventArgs.TransportMessage.MessageType, eventArgs.TransportMessage.MessageId, Environment.CurrentManagedThreadId);

        return Task.CompletedTask;
    }

    private Task OnTransportCreated(TransportEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace($"[OnTransportCreated] : uri = '{eventArgs.Transport.Uri}'");

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

    private Task OnTransportOperation(TransportOperationEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[{Scheme}.Operation] : transport name = '{TransportName}' / operation = '{Operation}' / managed thread id = {CurrentManagedThreadId}", eventArgs.Transport.Uri.Uri.Scheme, eventArgs.Transport.Uri.TransportName, eventArgs.Operation, Environment.CurrentManagedThreadId);

        return Task.CompletedTask;
    }
}