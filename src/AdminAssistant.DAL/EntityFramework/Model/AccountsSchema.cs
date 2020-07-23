using AdminAssistant.DAL.EntityFramework.Model.Accounts;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.EntityFramework.Model
{
    public interface IAccountsSchema
    {
        DbSet<BankAccountEntity> BankAccounts { get; set; }
        DbSet<BankAccountTransactionEntity> BankAccountTransactions { get; set; }
        DbSet<BankAccountTypeEntity> BankAccountTypes { get; set; }
    }
    internal class AccountsSchema : IAccountsSchema
    {
        private const string Name = "Accounts";

        public DbSet<BankAccountEntity> BankAccounts { get; set; } = null!;
        public DbSet<BankAccountTransactionEntity> BankAccountTransactions { get; set; } = null!;

        public DbSet<BankAccountTypeEntity> BankAccountTypes { get; set; } = null!;

        internal static void OnModelCreating(ModelBuilder modelBuilder)
        {
            AccountsSchema.BankAccount_OnModelCreating(modelBuilder);
            AccountsSchema.BankAccountTransaction_OnModelCreating(modelBuilder);
            AccountsSchema.BankAccountType_OnModelCreating(modelBuilder);
            AccountsSchema.Payee_OnModelCreating(modelBuilder);
        }

        private static void BankAccount_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccountEntity>().ToTable("BankAccount").Metadata.SetSchema(AccountsSchema.Name);
            modelBuilder.Entity<BankAccountEntity>().HasKey(x => x.BankAccountID);
            modelBuilder.Entity<BankAccountEntity>().HasOne(c => c.Currency).WithMany().HasForeignKey(x => x.CurrencyID);
            modelBuilder.Entity<BankAccountEntity>().Property(x => x.AccountName).IsRequired().IsUnicode().HasMaxLength(BankAccount.AccountNameMaxLength);
            modelBuilder.Entity<BankAccountEntity>().Property(x => x.CurrentBalance).IsRequired();
            modelBuilder.Entity<BankAccountEntity>().Property(x => x.CurrentBalance).IsRequired();
        }

        private static void BankAccountTransaction_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccountTransactionEntity>().ToTable("BankAccountTransaction").Metadata.SetSchema(AccountsSchema.Name);
            modelBuilder.Entity<BankAccountTransactionEntity>().HasKey(x => x.BankAccountTransactionID);
            modelBuilder.Entity<BankAccountTransactionEntity>().Property(x => x.Description).IsRequired().IsUnicode().HasMaxLength(BankAccountTransaction.DescriptionMaxLength);
            modelBuilder.Entity<BankAccountTransactionEntity>().Property(x => x.Notes).IsRequired().IsUnicode().HasMaxLength(Constants.NotesMaxLength);
            // TODO: BankAccountTransaction_OnModelCreating
        }

        private static void BankAccountType_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccountTypeEntity>().ToTable("BankAccountType").Metadata.SetSchema(AccountsSchema.Name);
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

        private static void Payee_OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PayeeEntity>().ToTable("Payee").Metadata.SetSchema(AccountsSchema.Name);
            modelBuilder.Entity<PayeeEntity>().HasKey(x => x.PayeeID);
            modelBuilder.Entity<PayeeEntity>().Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(Constants.NameMaxLength);
            // TODO: Payee_OnModelCreating
        }
    }
}
