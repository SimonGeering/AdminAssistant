using AdminAssistant.Infrastructure.EntityFramework.Model;
using AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;
using AdminAssistant.Infrastructure.EntityFramework.Model.Budget;
using AdminAssistant.Infrastructure.EntityFramework.Model.Contacts;
using AdminAssistant.Infrastructure.EntityFramework.Model.Core;
using AdminAssistant.Infrastructure.EntityFramework.Model.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace AdminAssistant.Infrastructure.EntityFramework;

// dotnet ef migrations add InitialCreate --startup-project ..\AdminAssistant.Accounts.Test\AdminAssistant.Accounts.Test.csproj
// dotnet ef database update
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
            
    }

    // Core ...
    public DbSet<AuditEntity> AuditTrail { get; set; } = null!;
    public DbSet<CompanyEntity> Company { get; set; } = null!;
    public DbSet<PersonalDetailsEntity> PersonalDetails { get; set; } = null!;
    public DbSet<OwnerEntity> Owners { get; set; } = null!;
    public DbSet<UserProfileEntity> UserProfiles { get; set; } = null!;
    public DbSet<CurrencyEntity> Currencies { get; set; } = null!;

    public DbSet<PermissionEntity> Permissions { get; set; } = null!;
    public DbSet<SettingEntity> Settings { get; set; } = null!;

    // Accounts ...
    public DbSet<BankEntity> Banks { get; set; } = null!;
    public DbSet<BankAccountEntity> BankAccounts { get; set; } = null!;
    public DbSet<BankAccountTransactionEntity> BankAccountTransactions { get; set; } = null!;
    public DbSet<BankAccountTypeEntity> BankAccountTypes { get; set; } = null!;

    // Contacts ...
    public DbSet<ContactEntity> Contacts { get; set; } = null!;

    // Documents ...
    public DbSet<DocumentEntity> Documents { get; set; } = null!;

    // Migrations etc ...
    public string ConnectionString => Database.GetDbConnection().ConnectionString;
    public void EnsureDatabaseIsCreated() => Database.EnsureCreated();
    public void Migrate() => Database.Migrate();

    public async Task EnsureDatabaseAsync(CancellationToken cancellationToken)
    {
        var dbCreator = Database.GetService<IRelationalDatabaseCreator>();

        var strategy = Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Create the database if it does not exist.
            // Do this first so there is then a database to start a transaction against.
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);
            }
        });
    }

    public async Task RunMigrationAsync(CancellationToken cancellationToken)
    {
        var strategy = Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
            await Database.MigrateAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }

    public async Task SeedDataAsync(CancellationToken cancellationToken)
    {
        var strategy = Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Seed the database
            await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
            // await Tickets.AddAsync(new()
            // {
            //     Title = "Test Ticket",
            //     Description = "Default ticket, please ignore!",
            //     Completed = true
            // }, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CoreSchema.OnModelCreating(modelBuilder);

        AccountsSchema.OnModelCreating(modelBuilder);
        AssetRegisterSchema.OnModelCreating(modelBuilder);
        BillingSchema.OnModelCreating(modelBuilder);
        BudgetSchema.OnModelCreating(modelBuilder);
        CalendarSchema.OnModelCreating(modelBuilder);
        ContactsSchema.OnModelCreating(modelBuilder);
        // DashboardSchema
        DocumentsSchema.OnModelCreating(modelBuilder);
        // MailSchema
        // ReportsSchema
        TasksSchema.OnModelCreating(modelBuilder);

        // No cascading deletes (Do this last) ...
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().Where(e => !e.IsOwned()).SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
