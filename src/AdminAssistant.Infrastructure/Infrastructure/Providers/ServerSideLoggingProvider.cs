using Microsoft.Extensions.Logging;

namespace AdminAssistant.Infrastructure.Providers;

internal sealed class ServerSideLoggingProvider(ILoggerFactory loggerFactory)
    : LoggingProvider(loggerFactory, ILoggingProvider.ServerSideLogCategory)
{
}
