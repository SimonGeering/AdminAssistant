using Microsoft.Extensions.Logging;

namespace AdminAssistant.Infra.Providers;

internal sealed class ServerSideLoggingProvider : LoggingProvider
{
    public ServerSideLoggingProvider(ILoggerFactory loggerFactory)
        : base(loggerFactory, ILoggingProvider.ServerSideLogCategory)
    {
    }
}
