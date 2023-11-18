using Microsoft.Extensions.Logging;

namespace AdminAssistant.Infra.Providers;

internal sealed class ServerSideLoggingProvider(ILoggerFactory loggerFactory)
    : LoggingProvider(loggerFactory, ILoggingProvider.ServerSideLogCategory)
{
}
