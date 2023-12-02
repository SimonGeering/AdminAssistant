using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.EntityFramework;

public sealed class PostgresApplicationDbContext(DbContextOptions<PostgresApplicationDbContext> options)
    : ApplicationDbContext(options, Shared.DatabaseProvider.PostgresSQL), IApplicationDbContext
{
}
