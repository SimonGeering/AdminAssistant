using SimonGeering.Framework;

namespace AdminAssistant.Shared;

public class UnsupportedDatabaseProviderException(DatabaseProvider databaseProvider)
    : ApplicationBaseException($"The given DatabaseProvider: {databaseProvider} is not supported in the current context.")
{
}
