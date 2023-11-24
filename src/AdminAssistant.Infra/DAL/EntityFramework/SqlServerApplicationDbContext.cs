using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.EntityFramework;

public sealed class SqlServerApplicationDbContext(DbContextOptions<SqlServerApplicationDbContext> options)
    : ApplicationDbContext(options, DomainModel.Shared.DatabaseProvider.SQLServer), IApplicationDbContext
{
}
