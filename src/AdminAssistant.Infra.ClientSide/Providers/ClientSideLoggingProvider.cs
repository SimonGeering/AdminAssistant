using Microsoft.Extensions.Logging;

namespace AdminAssistant.Infra.Providers;

internal sealed class ClientSideLoggingProvider : LoggingProvider
{
    public ClientSideLoggingProvider(ILoggerFactory loggerFactory)
        : base(loggerFactory, ILoggingProvider.ClientSideLogCategory)
    {
    }
}
