using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infrastructure.EntityFramework;

public sealed class SqlServerApplicationDbContext(DbContextOptions<SqlServerApplicationDbContext> options)
    : ApplicationDbContext(options, Shared.DatabaseProvider.SQLServer), IApplicationDbContext
{
}
