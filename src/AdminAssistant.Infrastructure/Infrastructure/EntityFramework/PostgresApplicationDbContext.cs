using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infrastructure.EntityFramework;

public sealed class PostgresApplicationDbContext(DbContextOptions<PostgresApplicationDbContext> options)
    : ApplicationDbContext(options, Shared.DatabaseProvider.PostgresSQL), IApplicationDbContext
{
}
