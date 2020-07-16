using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.EntityFramework.Model;
using AdminAssistant.DAL.EntityFramework.Model.Accounts;
using AdminAssistant.DAL.EntityFramework.Model.AssetRegister;
using AdminAssistant.DAL.EntityFramework.Model.Budget;
using AdminAssistant.DAL.EntityFramework.Model.Contacts;
using AdminAssistant.DAL.EntityFramework.Model.Documents;
using AdminAssistant.DomainModel.Infrastructure;
using AdminAssistant.DomainModel.Modules.Accounts;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.EntityFramework
{
    public interface IApplicationDbContext : IDisposable
    {
        //this.Address_OnModelCreating(modelBuilder);
        //this.Asset_OnModelCreating(modelBuilder);
        //this.Audit_OnModelCreating(modelBuilder);
        DbSet<BankAccountEntity> BankAccounts { get; set; }
        DbSet<BankAccountTransactionEntity> BankAccountTransactions { get; set; }
        DbSet<BankAccountTypeEntity> BankAccountTypes { get; set; }
        //this.Budget_OnModelCreating(modelBuilder);
        //this.BudgetEntry_OnModelCreating(modelBuilder);
        //this.Company_OnModelCreating(modelBuilder);
        //this.ContactAddress_OnModelCreating(modelBuilder);
        //this.Contact_OnModelCreating(modelBuilder);
        DbSet<CurrencyEntity> Currencies { get; set; }
        // this.Document_OnModelCreating(modelBuilder);
        // this.Owner_OnModelCreating(modelBuilder);
        // this.Payee_OnModelCreating(modelBuilder);
        // this.Permission_OnModelCreating(modelBuilder);
        // this.PersonalDetails_OnModelCreating(modelBuilder);
        // this.Setting_OnModelCreating(modelBuilder);
        DbSet<UserProfileEntity> UserProfiles { get; set; }
        // this.UserProfile_OnModelCreating(modelBuilder);
        // this.UserProfilePermission_OnModelCreating(modelBuilder);
        // this.UserProfileSetting_OnModelCreating(modelBuilder);
        //
        // TODO: Soft Delete https://medium.com/@unhandlederror/deleting-it-softly-with-ef-core-5f191db5cf72
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

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

        public DbSet<BankAccountEntity> BankAccounts { get; set; } = null!;
        public DbSet<BankAccountTransactionEntity> BankAccountTransactions { get; set; } = null!;

        public DbSet<BankAccountTypeEntity> BankAccountTypes { get; set; } = null!;
        public DbSet<CurrencyEntity> Currencies { get; set; } = null!;

        public DbSet<PermissionEntity> Permissions { get; set; } = null!;
        public DbSet<SettingEntity> Settings { get; set; } = null!;
        public DbSet<UserProfileEntity> UserProfiles { get; set; } = null!;

        public void EnsureDatabaseIsCreated() => this.Database.EnsureCreated();
        public void Migrate() => this.Database.Migrate();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.Address_OnModelCreating(modelBuilder);
            this.Asset_OnModelCreating(modelBuilder);
            this.Audit_OnModelCreating(modelBuilder);
            this.BankAccount_OnModelCreating(modelBuilder);
            this.BankAccountTransaction_OnModelCreating(modelBuilder);
            this.BankAccountTransaction_OnModelCreating(modelBuilder);
            this.BankAccountType_OnModelCreating(modelBuilder);
            this.Budget_OnModelCreating(modelBuilder);
            this.BudgetEntry_OnModelCreating(modelBuilder);
            this.Company_OnModelCreating(modelBuilder);
            this.ContactAddress_OnModelCreating(modelBuilder);
            this.Contact_OnModelCreating(modelBuilder);
            this.Currency_OnModelCreating(modelBuilder);
            this.Document_OnModelCreating(modelBuilder);
            this.Owner_OnModelCreating(modelBuilder);
            this.Payee_OnModelCreating(modelBuilder);
            this.Permission_OnModelCreating(modelBuilder);
            this.PersonalDetails_OnModelCreating(modelBuilder);
            this.Setting_OnModelCreating(modelBuilder);
            this.UserProfile_OnModelCreating(modelBuilder);
            this.UserProfilePermission_OnModelCreating(modelBuilder);
            this.UserProfileSetting_OnModelCreating(modelBuilder);

            this.RemoveCascadingDeletes(modelBuilder);
        }
        private void Address_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressEntity>().ToTable("Address");
            modelBuilder.Entity<AddressEntity>().HasKey(x => x.AddressID);
            // TODO: Address_OnModelCreating
        }
        private void Asset_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetEntity>().ToTable("Asset");
            modelBuilder.Entity<AssetEntity>().HasKey(x => x.AssetID);
            modelBuilder.Entity<AssetEntity>().Property(x => x.PurchasePrice).IsRequired();
            modelBuilder.Entity<AssetEntity>().Property(x => x.DepreciatedValue).IsRequired();
            modelBuilder.Entity<AssetEntity>().Property(x => x.ReplacementCost).IsRequired();
        }
        private void Audit_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditEntity>().ToTable("Audit");
            modelBuilder.Entity<AuditEntity>().HasKey(x => x.AuditID);
            modelBuilder.Entity<AuditEntity>()
                .HasOne(at => at.Asset)
                .WithOne(a => a.Audit)
                .HasForeignKey<AssetEntity>(x => x.AuditID);
            modelBuilder.Entity<AuditEntity>()
                .HasOne(ad => ad.Address)
                .WithOne(a => a.Audit)
                .HasForeignKey<AddressEntity>(x => x.AuditID);
            modelBuilder.Entity<AuditEntity>()
                .HasOne(pd => pd.PersonalDetails)
                .WithOne(a => a.Audit)
                .HasForeignKey<PersonalDetailsEntity>(x => x.AuditID);
            modelBuilder.Entity<AuditEntity>()
                .HasOne(c => c.Company)
                .WithOne(a => a.Audit)
                .HasForeignKey<CompanyEntity>(x => x.AuditID);
            modelBuilder.Entity<AuditEntity>()
                .HasOne(u => u.UserProfile)
                .WithOne(a => a.Audit)
                .HasForeignKey<UserProfileEntity>(x => x.AuditID);
            modelBuilder.Entity<AuditEntity>()
                .HasOne(b => b.BankAccount)
                .WithOne(a => a.Audit)
                .HasForeignKey<BankAccountEntity>(x => x.AuditID);

            modelBuilder.Entity<AuditEntity>().Property(x => x.IsArchived).IsRequired().HasDefaultValue(false);
            modelBuilder.Entity<AuditEntity>().Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);
            modelBuilder.Entity<AuditEntity>().Property(x => x.CreatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<AuditEntity>().Property(x => x.CreatedBy).IsRequired().IsUnicode().HasMaxLength(Constants.UserSignOnMaxLength);
            modelBuilder.Entity<AuditEntity>().Property(x => x.UpdatedBy).IsRequired().IsUnicode().HasMaxLength(Constants.UserSignOnMaxLength).HasDefaultValue(default(string));
            modelBuilder.Entity<AuditEntity>().Property(x => x.ArchivedBy).IsRequired().IsUnicode().HasMaxLength(Constants.UserSignOnMaxLength).HasDefaultValue(default(string));
            modelBuilder.Entity<AuditEntity>().Property(x => x.DeletedBy).IsRequired().IsUnicode().HasMaxLength(Constants.UserSignOnMaxLength).HasDefaultValue(default(string));
        }
        private void BankAccount_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccountEntity>().ToTable("BankAccount");
            modelBuilder.Entity<BankAccountEntity>().HasKey(x => x.BankAccountID);
            modelBuilder.Entity<BankAccountEntity>().HasOne(c => c.Currency).WithMany().HasForeignKey(x => x.CurrencyID);
            modelBuilder.Entity<BankAccountEntity>().Property(x => x.AccountName).IsRequired().IsUnicode().HasMaxLength(BankAccount.AccountNameMaxLength);
            modelBuilder.Entity<BankAccountEntity>().Property(x => x.CurrentBalance).IsRequired();
            modelBuilder.Entity<BankAccountEntity>().Property(x => x.CurrentBalance).IsRequired();
        }
        private void BankAccountTransaction_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccountTransactionEntity>().ToTable("BankAccountTransaction");
            modelBuilder.Entity<BankAccountTransactionEntity>().HasKey(x => x.BankAccountTransactionID);
            modelBuilder.Entity<BankAccountTransactionEntity>().Property(x => x.Description).IsRequired().IsUnicode().HasMaxLength(Constants.DescriptionMaxLength);
            modelBuilder.Entity<BankAccountTransactionEntity>().Property(x => x.Notes).IsRequired().IsUnicode().HasMaxLength(Constants.NotesMaxLength);
            // TODO: BankAccountTransaction_OnModelCreating
        }
        private void BankAccountType_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccountTypeEntity>().ToTable("BankAccountType");
            modelBuilder.Entity<BankAccountTypeEntity>().HasKey(x => x.BankAccountTypeID);
            modelBuilder.Entity<BankAccountTypeEntity>().Property(x => x.BankAccountTypeID).UseIdentityColumn();
            modelBuilder.Entity<BankAccountTypeEntity>().Property(x => x.Description).IsRequired().IsUnicode().HasMaxLength(Constants.DescriptionMaxLength);
            modelBuilder.Entity<BankAccountTypeEntity>().Property(x => x.AllowPersonal).IsRequired().HasDefaultValue(false);
            modelBuilder.Entity<BankAccountTypeEntity>().Property(x => x.AllowCompany).IsRequired().HasDefaultValue(false);
            modelBuilder.Entity<BankAccountTypeEntity>().Property(x => x.IsDeprecated).IsRequired().HasDefaultValue(false);

            modelBuilder.Entity<BankAccountTypeEntity>().HasData(new BankAccountTypeEntity[]
            {
                new BankAccountTypeEntity() { BankAccountTypeID = 1, Description = "Current Account", AllowCompany = true, AllowPersonal = true },
                new BankAccountTypeEntity() { BankAccountTypeID = 2, Description = "Savings Account", AllowCompany = true, AllowPersonal = true },
            });
        }
        private void Budget_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetEntity>().ToTable("Budget");
            modelBuilder.Entity<BudgetEntity>().HasKey(x => x.BudgetID);
            modelBuilder.Entity<BudgetEntity>().Property(x => x.BudgetName).IsRequired().IsUnicode().HasMaxLength(Budget.BudgetNameMaxLength);
            // TODO: Budget_OnModelCreating
        }
        private void BudgetEntry_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetEntryEntity>().ToTable("BudgetEntry");
            modelBuilder.Entity<BudgetEntryEntity>().HasKey(x => x.BudgetEntryID);
            // TODO: BudgetEntry_OnModelCreating
        }
        private void Company_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyEntity>().ToTable("Company");
            modelBuilder.Entity<CompanyEntity>().HasKey(x => x.CompanyID);
            modelBuilder.Entity<CompanyEntity>().Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(Constants.NameMaxLength);
            modelBuilder.Entity<CompanyEntity>().Property(x => x.CompanyNumber).IsRequired().IsUnicode().HasMaxLength(Company.CompanyNumberMaxLength).HasDefaultValue(default(string));
            modelBuilder.Entity<CompanyEntity>().Property(x => x.VATNumber).IsRequired().IsUnicode().HasMaxLength(Company.VATNumberMaxLength).HasDefaultValue(default(string));
        }
        private void ContactAddress_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactAddressEntity>().ToTable("ContactAddress");
            modelBuilder.Entity<ContactAddressEntity>().HasKey(x => x.ContactAddressID);
            // TODO: ContactAddress_OnModelCreating
        }
        private void Contact_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactEntity>().ToTable("Contact");
            modelBuilder.Entity<ContactEntity>().HasKey(x => x.ContactID);
            // TODO: Contact_OnModelCreating
        }
        private void Currency_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyEntity>().ToTable("Currency");
            modelBuilder.Entity<CurrencyEntity>().HasKey(x => x.CurrencyID);
            modelBuilder.Entity<CurrencyEntity>().Property(x => x.Symbol).IsRequired().IsUnicode().HasMaxLength(Currency.SymbolMaxLength).HasColumnType($"CHAR({Currency.SymbolMaxLength})");
            modelBuilder.Entity<CurrencyEntity>().Property(x => x.DecimalFormat).IsRequired().HasMaxLength(Currency.DecimalFormatMaxLength).HasColumnType($"CHAR({Currency.DecimalFormatMaxLength})");
            modelBuilder.Entity<CurrencyEntity>().Property(x => x.IsDeprecated).IsRequired().HasDefaultValue(false);
            modelBuilder.Entity<CurrencyEntity>().HasData(new CurrencyEntity[] 
            {
                new CurrencyEntity { CurrencyID = 1, Symbol = "GBP", DecimalFormat = "2.2-2" },
                new CurrencyEntity { CurrencyID = 2, Symbol = "EUR", DecimalFormat = "2.2-2" },
                new CurrencyEntity { CurrencyID = 3, Symbol = "USD", DecimalFormat = "2.2-2" },
            });
        }
        private void Document_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentEntity>().ToTable("Document");
            modelBuilder.Entity<DocumentEntity>().HasKey(x => x.DocumentID);
            // TODO: Document_OnModelCreating
        }
        private void Owner_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OwnerEntity>().ToTable("Owner");
            modelBuilder.Entity<OwnerEntity>().HasKey(x => x.OwnerID);
            modelBuilder.Entity<OwnerEntity>().HasIndex(x => new { x.CompanyID, x.PersonalDetailsID }).IsUnique();

            modelBuilder.Entity<OwnerEntity>()
                .HasOne(c => c.Company)
                .WithMany(o => o.Owns)
                .HasForeignKey(x => x.CompanyID);

            modelBuilder.Entity<OwnerEntity>()
                .HasOne(c => c.PersonalDetails)
                .WithMany(o => o.Owns)
                .HasForeignKey(x => x.PersonalDetailsID);

            modelBuilder.Entity<OwnerEntity>()
                .HasMany(b => b.BankAccounts)
                .WithOne(o => o.Owner)
                .HasForeignKey(x => x.OwnerID);
        }
        private void Payee_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PayeeEntity>().ToTable("Payee");
            modelBuilder.Entity<PayeeEntity>().HasKey(x => x.PayeeID);
            modelBuilder.Entity<PayeeEntity>().Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(Constants.NameMaxLength);
            // TODO: Payee_OnModelCreating
        }
        private void Permission_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermissionEntity>().ToTable("Permission");
            modelBuilder.Entity<PermissionEntity>().HasKey(x => x.PermissionID);
            modelBuilder.Entity<PermissionEntity>().HasIndex(x => x.PermissionKey).IsUnique();
            modelBuilder.Entity<PermissionEntity>().Property(x => x.PermissionKey).IsRequired().IsUnicode().HasMaxLength(Constants.KeyMaxLength);
        }
        private void PersonalDetails_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonalDetailsEntity>().ToTable("PersonalDetails");
            modelBuilder.Entity<PersonalDetailsEntity>().HasKey(x => x.PersonalDetailsID);
            // TODO: PersonalDetails_OnModelCreating
        }
        private void Setting_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SettingEntity>().ToTable("Setting");
            modelBuilder.Entity<SettingEntity>().HasKey(x => x.SettingID);
            modelBuilder.Entity<SettingEntity>().HasIndex(x => x.SettingKey).IsUnique();
            modelBuilder.Entity<SettingEntity>().Property(x => x.SettingKey).IsRequired().IsUnicode().HasMaxLength(Constants.KeyMaxLength);
        }
        private void UserProfile_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfileEntity>().ToTable("UserProfile");
            modelBuilder.Entity<UserProfileEntity>().HasKey(x => x.UserProfileID);
            modelBuilder.Entity<UserProfileEntity>()
                .HasMany(c => c.Companies)
                .WithOne(up => up.UserProfile)
                .HasForeignKey(x => x.UserProfileID).IsRequired();
            modelBuilder.Entity<UserProfileEntity>()
                .HasOne(pd => pd.PersonalDetails)
                .WithOne(up => up.UserProfile)
                .HasForeignKey<PersonalDetailsEntity>(x => x.UserProfileID).IsRequired();
            modelBuilder.Entity<UserProfileEntity>().HasIndex(x => x.SignOn).IsUnique();
            modelBuilder.Entity<UserProfileEntity>().Property(x => x.SignOn).IsRequired().IsUnicode().HasMaxLength(User.SignOnMaxLength);
            modelBuilder.Entity<UserProfileEntity>().Property(x => x.MSGraphID).IsRequired().IsUnicode().HasMaxLength(Constants.MSGraphIDMaxLength).HasDefaultValue(default(string));
        }
        private void UserProfilePermission_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfilePermissionEntity>().ToTable("UserProfilePermission");
            modelBuilder.Entity<UserProfilePermissionEntity>().HasKey(x => x.UserProfilePermissionID);
            modelBuilder.Entity<UserProfilePermissionEntity>().HasIndex(x => new { x.UserProfileID, x.PermissionID }).IsUnique();
            modelBuilder.Entity<UserProfilePermissionEntity>()
                .HasOne(up => up.UserProfile)
                .WithMany(p => p.Permissions)
                .HasForeignKey(x => x.UserProfileID).IsRequired();
            modelBuilder.Entity<UserProfilePermissionEntity>()
                .HasOne(p => p.Permission)
                .WithMany(upp => upp.UserProfilePermissions)
                .HasForeignKey(x => x.PermissionID).IsRequired();
        }
        private void UserProfileSetting_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfileSettingEntity>().ToTable("UserProfileSetting");
            modelBuilder.Entity<UserProfileSettingEntity>().HasKey(x => x.UserProfileSettingID);
            modelBuilder.Entity<UserProfileSettingEntity>().HasIndex(x => new { x.UserProfileID, x.SettingID }).IsUnique();
            modelBuilder.Entity<UserProfileSettingEntity>()
                .HasOne(ups => ups.UserProfile)
                .WithMany(s => s.Settings)
                .HasForeignKey(x => x.UserProfileID).IsRequired();
            modelBuilder.Entity<UserProfileSettingEntity>()
                .HasOne(s => s.Setting)
                .WithMany(upp => upp.UserProfileSettings)
                .HasForeignKey(x => x.SettingID).IsRequired();
        }

        private void RemoveCascadingDeletes(ModelBuilder modelBuilder)
        {
            // No cascading deletes ...
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().Where(e => !e.IsOwned()).SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
