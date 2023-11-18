using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.EntityFramework;

public sealed class SqliteApplicationDbContext(DbContextOptions<SqliteApplicationDbContext> options)
    : ApplicationDbContext(options, DomainModel.Shared.DatabaseProvider.SQLite), IApplicationDbContext
{
}
