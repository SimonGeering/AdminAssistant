using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infrastructure.EntityFramework;

public sealed class SqliteApplicationDbContext(DbContextOptions<SqliteApplicationDbContext> options)
    : ApplicationDbContext(options, Shared.DatabaseProvider.SQLite), IApplicationDbContext
{
}
