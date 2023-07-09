using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.EntityFramework;

public sealed class PostgresApplicationDbContext : ApplicationDbContext, IApplicationDbContext
{
    public PostgresApplicationDbContext(DbContextOptions<PostgresApplicationDbContext> options)
        : base(options, DomainModel.Shared.DatabaseProvider.PostgresSQL)
    {
    }
}
