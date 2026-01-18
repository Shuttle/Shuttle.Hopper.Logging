using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shuttle.Core.Contract;

namespace Shuttle.Hopper.Logging;

public class TransportEventLogger : IHostedService
{
    private readonly ILogger<TransportEventLogger> _logger;
    private readonly HopperOptions _hopperOptions;

    public TransportEventLogger(ILogger<TransportEventLogger> logger, IOptions<HopperOptions> hopperOptions)
    {
        _hopperOptions = Guard.AgainstNull(Guard.AgainstNull(hopperOptions).Value);
        _logger = Guard.AgainstNull(logger);

        _hopperOptions.TransportCreated += TransportCreated;
        _hopperOptions.TransportDisposing += TransportDisposing;
        _hopperOptions.TransportDisposed += TransportDisposed;
        _hopperOptions.MessageAcknowledged += MessageAcknowledged;
        _hopperOptions.MessageSent += MessageSent;
        _hopperOptions.MessageReceived += MessageReceived;
        _hopperOptions.MessageReleased += MessageReleased;
        _hopperOptions.TransportOperation += TransportOperation;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _hopperOptions.TransportCreated -= TransportCreated;
        _hopperOptions.TransportDisposing -= TransportDisposing;
        _hopperOptions.TransportDisposed -= TransportDisposed;
        _hopperOptions.MessageAcknowledged -= MessageAcknowledged;
        _hopperOptions.MessageSent -= MessageSent;
        _hopperOptions.MessageReceived -= MessageReceived;
        _hopperOptions.MessageReleased -= MessageReleased;
        _hopperOptions.TransportOperation -= TransportOperation;

        return Task.CompletedTask;
    }

    private Task MessageAcknowledged(MessageAcknowledgedEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[{Scheme}.MessageAcknowledged] : transport name = '{TransportName}' / managed thread id = {CurrentManagedThreadId}", eventArgs.Transport.Uri.Uri.Scheme, eventArgs.Transport.Uri.TransportName, Environment.CurrentManagedThreadId);

        return Task.CompletedTask;
    }

    private Task MessageReceived(MessageReceivedEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[{Scheme}.MessageReceived] : transport name = '{TransportName}' / managed thread id = {CurrentManagedThreadId}", eventArgs.Transport.Uri.Uri.Scheme, eventArgs.Transport.Uri.TransportName, Environment.CurrentManagedThreadId);

        return Task.CompletedTask;
    }

    private Task MessageReleased(MessageReleasedEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[{Scheme}.MessageReleased] : transport name = '{TransportName}' / managed thread id = {CurrentManagedThreadId}", eventArgs.Transport.Uri.Uri.Scheme, eventArgs.Transport.Uri.TransportName, Environment.CurrentManagedThreadId);

        return Task.CompletedTask;
    }

    private Task MessageSent(MessageSentEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[{Scheme}.MessageEnqueued] : transport name = '{TransportName}' / message type = '{MessageType}' / message id = '{MessageId}' / managed thread id = {CurrentManagedThreadId}", eventArgs.Transport.Uri.Uri.Scheme, eventArgs.Transport.Uri.TransportName, eventArgs.TransportMessage.MessageType, eventArgs.TransportMessage.MessageId, Environment.CurrentManagedThreadId);

        return Task.CompletedTask;
    }

    private Task TransportCreated(TransportEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace($"[OnTransportCreated] : uri = '{eventArgs.Transport.Uri}'");

        return Task.CompletedTask;
    }

    private Task TransportDisposed(TransportEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace($"[OnTransportDisposed] : uri = '{eventArgs.Transport.Uri}'");

        return Task.CompletedTask;
    }

    private Task TransportDisposing(TransportEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace($"[OnTransportDisposing] : uri = '{eventArgs.Transport.Uri}'");

        return Task.CompletedTask;
    }

    private Task TransportOperation(TransportOperationEventArgs eventArgs, CancellationToken cancellationToken)
    {
        _logger.LogTrace("[{Scheme}.Operation] : transport name = '{TransportName}' / operation = '{Operation}' / managed thread id = {CurrentManagedThreadId}", eventArgs.Transport.Uri.Uri.Scheme, eventArgs.Transport.Uri.TransportName, eventArgs.Operation, Environment.CurrentManagedThreadId);

        return Task.CompletedTask;
    }
}