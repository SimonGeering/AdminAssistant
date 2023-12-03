using Microsoft.Extensions.Logging;

namespace AdminAssistant.Infrastructure.Providers;

internal sealed class ClientSideLoggingProvider(ILoggerFactory loggerFactory)
    : LoggingProvider(loggerFactory, ILoggingProvider.ClientSideLogCategory)
{
}
