namespace Shuttle.Hopper.Logging;

public class ServiceBusLoggingOptions
{
    public List<string> PipelineEventTypes { get; set; } = [];
    public List<string> PipelineTypes { get; set; } = [];
    public bool QueueEvents { get; set; }
    public bool Threading { get; set; }
    public bool TransportMessageDeferred { get; set; }
}