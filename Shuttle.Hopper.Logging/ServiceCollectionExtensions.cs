using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Hopper.Logging;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddServiceBusLogging(Action<ServiceBusLoggingBuilder>? builder = null)
        {
            var serviceBusLoggingBuilder = new ServiceBusLoggingBuilder(Guard.AgainstNull(services));

            builder?.Invoke(serviceBusLoggingBuilder);

            services.AddOptions<ServiceBusLoggingOptions>().Configure(options =>
            {
                options.PipelineTypes = serviceBusLoggingBuilder.Options.PipelineTypes;
                options.PipelineEventTypes = serviceBusLoggingBuilder.Options.PipelineEventTypes;
                options.QueueEvents = serviceBusLoggingBuilder.Options.QueueEvents;
                options.TransportMessageDeferred = serviceBusLoggingBuilder.Options.TransportMessageDeferred;
                options.Threading = serviceBusLoggingBuilder.Options.Threading;
            });

            services.AddHostedService<TransportEventLogger>();
            services.AddHostedService<TransportMessageDeferredLogger>();
            services.AddHostedService<StartupPipelineLogger>();
            services.AddHostedService<ShutdownPipelineLogger>();
            services.AddHostedService<InboxMessagePipelineLogger>();
            services.AddHostedService<OutboxPipelineLogger>();
            services.AddHostedService<DeferredMessagePipelineLogger>();
            services.AddHostedService<DispatchTransportMessagePipelineLogger>();
            services.AddHostedService<TransportMessagePipelineLogger>();

            services.AddSingleton<IServiceBusLoggingConfiguration, ServiceBusLoggingConfiguration>();

            return services;
        }
    }
}