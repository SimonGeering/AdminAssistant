using SimonGeering.Framework;

namespace AdminAssistant.Shared;

public enum DatabaseProvider
{
    Unknown,
    SQLServer,
    SQLServerLocalDB,
    SQLite,
    PostgresSQL,
    SQLServerContainer,
    PostgresSQLContainer
}
