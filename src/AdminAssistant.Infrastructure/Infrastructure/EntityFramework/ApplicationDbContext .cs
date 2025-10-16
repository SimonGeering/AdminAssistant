using AdminAssistant.Shared;
using AdminAssistant.Infrastructure.EntityFramework.Model;
using AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;
using AdminAssistant.Infrastructure.EntityFramework.Model.Budget;
using AdminAssistant.Infrastructure.EntityFramework.Model.Contacts;
using AdminAssistant.Infrastructure.EntityFramework.Model.Core;
using AdminAssistant.Infrastructure.EntityFramework.Model.Documents;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infrastructure.EntityFramework;

public interface IApplicationDbContext : IDisposable
{
    // Core ...
    DbSet<AuditEntity> AuditTrail { get; set; }
    DbSet<CompanyEntity> Company { get; set; }
    DbSet<PersonalDetailsEntity> PersonalDetails { get; set; }
    DbSet<OwnerEntity> Owners { get; set; }

    DbSet<UserProfileEntity> UserProfiles { get; set; }
    DbSet<CurrencyEntity> Currencies { get; set; }

    DbSet<PermissionEntity> Permissions { get; set; }
    DbSet<SettingEntity> Settings { get; set; }

    // Accounts ...
    DbSet<BankEntity> Banks { get; set; }
    DbSet<BankAccountEntity> BankAccounts { get; set; }
    DbSet<BankAccountTransactionEntity> BankAccountTransactions { get; set; }
    DbSet<BankAccountTypeEntity> BankAccountTypes { get; set; }

    // Contacts ...
    DbSet<ContactEntity> Contacts { get; set; }

    // Documents ...
    DbSet<DocumentEntity> Documents { get; set; }

    // TODO: Soft Delete https://medium.com/@unhandlederror/deleting-it-softly-with-ef-core-5f191db5cf72
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    // Migrations etc ...
    string ConnectionString { get; }
    void EnsureDatabaseIsCreated();
    void Migrate();
}

// dotnet ef migrations add InitialCreate --startup-project ..\AdminAssistant.Accounts.Test\AdminAssistant.Accounts.Test.csproj
// dotnet ef database update
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
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
