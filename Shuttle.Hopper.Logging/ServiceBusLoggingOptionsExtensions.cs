using Shuttle.Core.Contract;

namespace Shuttle.Hopper.Logging;

public static class ServiceBusLoggingOptionsExtensions
{
    extension(ServiceBusLoggingOptions serviceBusLoggingOptions)
    {
        public ServiceBusLoggingOptions AddPipelineEventType<T>()
        {
            return serviceBusLoggingOptions.AddPipelineEventType(typeof(T));
        }

        public ServiceBusLoggingOptions AddPipelineEventType(Type type)
        {
            if (serviceBusLoggingOptions.PipelineEventTypes == null)
            {
                throw new InvalidOperationException(Resources.PipelineTypesNullException);
            }

            serviceBusLoggingOptions.PipelineEventTypes.Add(Guard.AgainstEmpty(Guard.AgainstNull(type).AssemblyQualifiedName));

            return serviceBusLoggingOptions;
        }

        public ServiceBusLoggingOptions AddPipelineType<T>()
        {
            return serviceBusLoggingOptions.AddPipelineType(typeof(T));
        }

        public ServiceBusLoggingOptions AddPipelineType(Type type)
        {
            if (serviceBusLoggingOptions.PipelineTypes == null)
            {
                throw new InvalidOperationException(Resources.PipelineTypesNullException);
            }

            serviceBusLoggingOptions.PipelineTypes.Add(Guard.AgainstEmpty(Guard.AgainstNull(type).AssemblyQualifiedName));

            return serviceBusLoggingOptions;
        }
    }
}