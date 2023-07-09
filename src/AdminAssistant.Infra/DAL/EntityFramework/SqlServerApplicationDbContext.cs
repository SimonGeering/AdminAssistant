using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.EntityFramework;

public sealed class SqlServerApplicationDbContext : ApplicationDbContext, IApplicationDbContext
{
    public SqlServerApplicationDbContext(DbContextOptions<SqlServerApplicationDbContext> options)
        : base(options, DomainModel.Shared.DatabaseProvider.SQLServer)
    {
    }
}
