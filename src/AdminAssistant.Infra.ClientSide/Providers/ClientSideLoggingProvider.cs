using Microsoft.Extensions.Logging;

namespace AdminAssistant.Infra.Providers
{
    internal class ClientSideLoggingProvider : LoggingProvider, ILoggingProvider
    {
        public ClientSideLoggingProvider(ILoggerFactory loggerFactory)
            : base(loggerFactory, ILoggingProvider.ClientSideLogCategory)
        {
        }
    }
}
