using Microsoft.Extensions.Logging;

namespace AdminAssistant.Infra.Providers;

internal sealed class ClientSideLoggingProvider(ILoggerFactory loggerFactory)
    : LoggingProvider(loggerFactory, ILoggingProvider.ClientSideLogCategory)
{
}
