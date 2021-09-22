using AdminAssistant.DomainModel.Shared;
using AdminAssistant.DomainModel.Modules.CoreModule;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Core;

public static class CoreSchema
{
    public const string DefaultCurrencyDecimalFormat = "2.2-2";

    private const string Name = "Core";

    /// <summary>Sets up static lookup data for the core module.</summary>
    /// <remarks>
    /// This is used both in the EF Core migrations for the prod DB as well as functional acceptance and integration
    /// tests. In the latter case, it simulate migrations having been run, because the `Respawn` NuGet package
    /// clears down the DB to blank between each test execution.
    /// </remarks>
    /// <param name="includeIDs">`True` for EF Core migration code otherwise `False`.</param>
    /// <returns>Out of the box default currencies.</returns>
    public static CurrencyEntity[] GetCurrencySeedData(bool includeIDs = false)
    {
        var GBP = new CurrencyEntity { Symbol = "GBP", DecimalFormat = DefaultCurrencyDecimalFormat };
        if (includeIDs) GBP.CurrencyID = 1;

        var EUR = new CurrencyEntity { Symbol = "EUR", DecimalFormat = DefaultCurrencyDecimalFormat };
        if (includeIDs) EUR.CurrencyID = 2;

        var USD = new CurrencyEntity { Symbol = "USD", DecimalFormat = DefaultCurrencyDecimalFormat };
        if (includeIDs) USD.CurrencyID = 3;

        return new CurrencyEntity[] { GBP, EUR, USD };
    }

    internal static void OnModelCreating(ModelBuilder modelBuilder)
    {
        CoreSchema.Audit_OnModelCreating(modelBuilder);
        CoreSchema.Company_OnModelCreating(modelBuilder);
        CoreSchema.Currency_OnModelCreating(modelBuilder);
        CoreSchema.Owner_OnModelCreating(modelBuilder);
        CoreSchema.Permission_OnModelCreating(modelBuilder);
        CoreSchema.PersonalDetails_OnModelCreating(modelBuilder);
        CoreSchema.Setting_OnModelCreating(modelBuilder);
        CoreSchema.UserProfile_OnModelCreating(modelBuilder);
        CoreSchema.UserProfilePermission_OnModelCreating(modelBuilder);
        CoreSchema.UserProfileSetting_OnModelCreating(modelBuilder);
    }

    private static void Audit_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditEntity>().ToTable("Audit").Metadata.SetSchema(CoreSchema.Name);
        modelBuilder.Entity<AuditEntity>().HasKey(x => x.AuditID);
        modelBuilder.Entity<AuditEntity>()
            .HasOne(at => at.Asset)
            .WithOne(a => a.Audit)
            .HasForeignKey<AssetRegister.AssetEntity>(x => x.AuditID);
        modelBuilder.Entity<AuditEntity>()
            .HasOne(ad => ad.Address)
            .WithOne(a => a.Audit)
            .HasForeignKey<Contacts.AddressEntity>(x => x.AuditID);
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
            .HasForeignKey<Accounts.BankAccountEntity>(x => x.AuditID);

        modelBuilder.Entity<AuditEntity>().Property(x => x.IsArchived).IsRequired().HasDefaultValue(false);
        modelBuilder.Entity<AuditEntity>().Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);
        modelBuilder.Entity<AuditEntity>().Property(x => x.CreatedOn).IsRequired().HasDefaultValueSql("GETDATE()");
        modelBuilder.Entity<AuditEntity>().Property(x => x.CreatedBy).IsRequired().IsUnicode().HasMaxLength(Constants.UserSignOnMaxLength);
        modelBuilder.Entity<AuditEntity>().Property(x => x.UpdatedBy).IsRequired().IsUnicode().HasMaxLength(Constants.UserSignOnMaxLength).HasDefaultValue(default(string));
        modelBuilder.Entity<AuditEntity>().Property(x => x.ArchivedBy).IsRequired().IsUnicode().HasMaxLength(Constants.UserSignOnMaxLength).HasDefaultValue(default(string));
        modelBuilder.Entity<AuditEntity>().Property(x => x.DeletedBy).IsRequired().IsUnicode().HasMaxLength(Constants.UserSignOnMaxLength).HasDefaultValue(default(string));
    }

    private static void Company_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyEntity>().ToTable("Company").Metadata.SetSchema(CoreSchema.Name);
        modelBuilder.Entity<CompanyEntity>().HasKey(x => x.CompanyID);
        modelBuilder.Entity<CompanyEntity>().Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(Constants.NameMaxLength);
        modelBuilder.Entity<CompanyEntity>().Property(x => x.CompanyNumber).IsRequired().IsUnicode().HasMaxLength(Company.CompanyNumberMaxLength).HasDefaultValue(default(string));
        modelBuilder.Entity<CompanyEntity>().Property(x => x.VATNumber).IsRequired().IsUnicode().HasMaxLength(Company.VATNumberMaxLength).HasDefaultValue(default(string));
    }

    private static void Currency_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyEntity>().ToTable("Currency").Metadata.SetSchema(CoreSchema.Name);
        modelBuilder.Entity<CurrencyEntity>().HasKey(x => x.CurrencyID);
        modelBuilder.Entity<CurrencyEntity>().Property(x => x.Symbol).IsRequired().IsUnicode().HasMaxLength(Currency.SymbolMaxLength).HasColumnType($"CHAR({Currency.SymbolMaxLength})");
        modelBuilder.Entity<CurrencyEntity>().Property(x => x.DecimalFormat).IsRequired().HasMaxLength(Currency.DecimalFormatMaxLength).HasColumnType($"CHAR({Currency.DecimalFormatMaxLength})");
        modelBuilder.Entity<CurrencyEntity>().Property(x => x.IsDeprecated).IsRequired().HasDefaultValue(false);
        modelBuilder.Entity<CurrencyEntity>().HasData(GetCurrencySeedData(true));
    }

    private static void Owner_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OwnerEntity>().ToTable("Owner").Metadata.SetSchema(CoreSchema.Name);
        modelBuilder.Entity<OwnerEntity>().HasKey(x => x.OwnerID);
        modelBuilder.Entity<OwnerEntity>().HasIndex(x => new { x.CompanyID, x.PersonalDetailsID }).IsUnique();

        modelBuilder.Entity<OwnerEntity>()
            .HasOne(c => c.Company)
            .WithMany(o => o.Owns).IsRequired(false)
            .HasForeignKey(x => x.CompanyID);

        modelBuilder.Entity<OwnerEntity>()
            .HasOne(c => c.PersonalDetails)
            .WithMany(o => o.Owns).IsRequired(false)
            .HasForeignKey(x => x.PersonalDetailsID);

        modelBuilder.Entity<OwnerEntity>()
            .HasMany(b => b.BankAccounts)
            .WithOne(o => o.Owner)
            .HasForeignKey(x => x.OwnerID);
    }

    private static void Permission_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PermissionEntity>().ToTable("Permission").Metadata.SetSchema(CoreSchema.Name);
        modelBuilder.Entity<PermissionEntity>().HasKey(x => x.PermissionID);
        modelBuilder.Entity<PermissionEntity>().HasIndex(x => x.PermissionKey).IsUnique();
        modelBuilder.Entity<PermissionEntity>().Property(x => x.PermissionKey).IsRequired().IsUnicode().HasMaxLength(Constants.KeyMaxLength);
    }

    private static void PersonalDetails_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonalDetailsEntity>().ToTable("PersonalDetails").Metadata.SetSchema(CoreSchema.Name);
        modelBuilder.Entity<PersonalDetailsEntity>().HasKey(x => x.PersonalDetailsID);
        // TODO: PersonalDetails_OnModelCreating
    }

    private static void Setting_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SettingEntity>().ToTable("Setting").Metadata.SetSchema(CoreSchema.Name);
        modelBuilder.Entity<SettingEntity>().HasKey(x => x.SettingID);
        modelBuilder.Entity<SettingEntity>().HasIndex(x => x.SettingKey).IsUnique();
        modelBuilder.Entity<SettingEntity>().Property(x => x.SettingKey).IsRequired().IsUnicode().HasMaxLength(Constants.KeyMaxLength);
    }

    private static void UserProfile_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProfileEntity>().ToTable("UserProfile").Metadata.SetSchema(CoreSchema.Name);
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

    private static void UserProfilePermission_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProfilePermissionEntity>().ToTable("UserProfilePermission").Metadata.SetSchema(CoreSchema.Name);
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

    private static void UserProfileSetting_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProfileSettingEntity>().ToTable("UserProfileSetting").Metadata.SetSchema(CoreSchema.Name);
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
}
