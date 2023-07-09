using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.EntityFramework;

public sealed class SqliteApplicationDbContext : ApplicationDbContext, IApplicationDbContext
{
    public SqliteApplicationDbContext(DbContextOptions<SqliteApplicationDbContext> options)
        : base(options, DomainModel.Shared.DatabaseProvider.SQLite)
    {
    }
}
