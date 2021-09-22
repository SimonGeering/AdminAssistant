using AdminAssistant.Infra.DAL.EntityFramework.Model;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Budget;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Core;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Documents;
using Microsoft.EntityFrameworkCore;


/*
project Admin_Assistant
{
  database_type: "SQL Server"
}

//
// ==============================
// Core Module 
// ==============================
//
Table "Core.Audit" 
{
  "AuditID" INT [pk, increment]
  "IsArchived" BIT [not null]
  "IsDeleted" BIT [not null]
  "CreatedOn" DATETIME2 [not null]
  "CreatedBy" NVARCHAR(50) [not null]
  "ReplacementCost" INT
  "UpdatedOn" DATETIME2
  "UpdatedBy" NVARCHAR(50) [not null]
  "ArchivedOn" DATETIME2
  "ArchivedBy" NVARCHAR(50) [not null]
  "DeletedOn" DATETIME2
  "DeletedBy" NVARCHAR(50) [not null]
}

Table "Core.Owner" 
{
  "OwnerID" INT [pk, increment]
  "CompanyID" INT
  "PersonalDetailsID" INT
}

Table "Core.Company" 
{
  "CompanyID" INT [pk, increment]
  "AuditID" INT
  "UserProfileID" INT
  "Name" NVARCHAR(50)
  "CompanyNumber" NVARCHAR(50)
  "VATNumber" NVARCHAR(50)
  "DateOfIncorporation" DATETIME2
}
Ref: "Core.Company"."CompanyID" < "Core.Owner"."CompanyID"

Table "Core.UserProfile" 
{
  "UserProfileID" INT [pk, increment]
  "AuditID" INT
  "SignOn" NVARCHAR(50)
  "MSGraphID" NVARCHAR(50)
}
Ref: "Core.UserProfile"."UserProfileID" < "Core.PersonalDetails"."UserProfileID"
Ref: "Core.UserProfile"."UserProfileID" < "Core.UserProfilePermission"."UserProfileID"
Ref: "Core.UserProfile"."UserProfileID" < "Core.Company"."UserProfileID"
Ref: "Core.UserProfile"."UserProfileID" < "Core.UserProfileSetting"."UserProfileID"

Table "Core.Permission" 
{
  "PermissionID" INT [pk]
  "PermissionKey" NVARCHAR(20)
}
Ref: "Core.Permission"."PermissionID" < "Core.UserProfilePermission"."PermissionID"

Table "Core.PersonalDetails" 
{
  "PersonalDetailsID" INT [pk]
  "AuditID" INT
  "UserProfileID" NVARCHAR(255)
}
Ref: "Core.PersonalDetails"."PersonalDetailsID" < "Core.Owner"."PersonalDetailsID"

Table "Core.UserProfileSetting" 
{
  "UserProfileSettingID" INT [pk]
  "UserProfileID" INT
  "SettingID" INT
}
Table "Core.UserProfilePermission" 
{
  "UserProfilePermissionID" INT [pk]
  "UserProfileID" INT
  "PermissionID" INT
}

Table "Core.Setting" 
{
  "SettingID" INT [pk]
  "SettingKey" NVARCHAR(20)
}
Ref: "Core.Setting"."SettingID" < "Core.UserProfileSetting"."SettingID"

Table "Core.Currency" 
{
  "CurrencyID" INT [pk]
  "IsDeprecated" BIT
  "Symbol" CHAR(3)
  "DecimalFormat" CHAR(5)
}
Ref: "Core.Currency"."CurrencyID" < "Accounts.BankAccount"."CurrencyID"
Ref: "Core.Currency"."CurrencyID" < "Accounts.BankAccountTransaction"."CurrencyID"

//
// ==============================
// Accounts Module
// ==============================
//
Table "Accounts.Bank" 
{
  "BankID" INT [pk]
  "AuditID" INT
  "Name" NVRCHAR(50)
}
Ref: "Accounts.Bank"."BankID" < "Accounts.BankAccount"."BankID"

Table "Accounts.BankAccountType" 
{
  "BankAccountTypeID" INT [pk]
  "IsDeprecated" BIT
  "Description" NVARCHAR(255)
  "AllowPersonal" BIT
  "AllowCompany" BIT
}
Ref: "Accounts.BankAccountType"."BankAccountTypeID" < "Accounts.BankAccount"."BankAccountTypeID"

Table "Accounts.BankAccount" 
{
  "BankAccountID" INT [pk]
  "AuditID" INT
  "OwnerID" INT
  "BankID" INT
  "BankAccountTypeID" INT
  "CurrencyID" INT
  "AccountName" NVARCHAR(50)
  "OpeningBalance" INT
  "CurrentBalance" INT
  "OpenedOn" DATETIME2
  "IsBudgeted" BIT
}
Ref: "Accounts.BankAccount"."BankAccountID" < "Accounts.BankAccountTransaction"."BankAccountID"

Table "Accounts.BankAccountTransaction" 
{
  "BankAccountTransactionID" INT [pk]
  "AuditID" INT
  "BankAccountID" INT
  "PayeeID" INT
  "CurrencyID" INT
  "BankAccountTransactionTypeID" INT
  "BankAccountStatementID" INT
  "BankAccountStatementNumber" INT
  "IsReconciled" BIT
  "TransactionDate" DATETIME2
  "Credit" INT
  "Debit" INT
  "Description" NVARCHAR(255)
  "Notes" NVARCHAR(4000)
}

Table "Accounts.Payee" 
{
  "PayeeID" INT [pk]
  "AuditID" INT
  "Name" NVARCHAR(255)
}
Ref: "Accounts.Payee"."PayeeID" < "Accounts.BankAccountTransaction"."PayeeID"
Ref: "Accounts.Payee"."PayeeID" < "Accounts.PayeeContact"."PayeeID"

Table "Accounts.PayeeContact" 
{
  "PayeeContactID" INT [pk]
  "AuditID" INT
  "PayeeID" INT
  "ContactID" INT
  "IsPrimaryContact" BIT
}

Table "Accounts.BankAccountTransactionType" 
{
  "BankAccountTransactionTypeID" INT [pk]
  "IsDeprecated" BIT
  "Description" NVARCHAR(255)
}
Ref: "Accounts.BankAccountTransactionType"."BankAccountTransactionTypeID" < "Accounts.BankAccountTransaction"."BankAccountTransactionTypeID"

*/

namespace AdminAssistant.Infra.DAL.EntityFramework;

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
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
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
