using AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model
{
    internal class AccountsSchema
    {
        private const string Name = "Accounts";

        internal static void OnModelCreating(ModelBuilder builder)
        {
            AccountsSchema.Bank_OnModelCreating(builder);
            AccountsSchema.BankAccount_OnModelCreating(builder);
            AccountsSchema.BankAccountTransaction_OnModelCreating(builder);
            AccountsSchema.BankAccountType_OnModelCreating(builder);
            AccountsSchema.Payee_OnModelCreating(builder);
        }

        private static void Bank_OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BankEntity>().ToTable("Bank").Metadata.SetSchema(AccountsSchema.Name);
            builder.Entity<BankEntity>().HasKey(x => x.BankID);
            builder.Entity<BankEntity>().Property(x => x.BankName).IsRequired().IsUnicode().HasMaxLength(Bank.BankNameMaxLength);
        }

        private static void BankAccount_OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BankAccountEntity>().ToTable("BankAccount").Metadata.SetSchema(AccountsSchema.Name);
            builder.Entity<BankAccountEntity>().HasKey(x => x.BankAccountID);
            builder.Entity<BankAccountEntity>().HasOne(c => c.Currency).WithMany().HasForeignKey(x => x.CurrencyID);
            builder.Entity<BankAccountEntity>().Property(x => x.AccountName).IsRequired().IsUnicode().HasMaxLength(BankAccount.AccountNameMaxLength);
            builder.Entity<BankAccountEntity>().Property(x => x.CurrentBalance).IsRequired();
            builder.Entity<BankAccountEntity>().Property(x => x.CurrentBalance).IsRequired();
        }

        private static void BankAccountTransaction_OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BankAccountTransactionEntity>().ToTable("BankAccountTransaction").Metadata.SetSchema(AccountsSchema.Name);
            builder.Entity<BankAccountTransactionEntity>().HasKey(x => x.BankAccountTransactionID);
            builder.Entity<BankAccountTransactionEntity>().Property(x => x.Description).IsRequired().IsUnicode().HasMaxLength(BankAccountTransaction.DescriptionMaxLength);
            builder.Entity<BankAccountTransactionEntity>().Property(x => x.Notes).IsRequired().IsUnicode().HasMaxLength(Constants.NotesMaxLength);
            // TODO: BankAccountTransaction_OnModelCreating
        }

        private static void BankAccountType_OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BankAccountTypeEntity>().ToTable("BankAccountType").Metadata.SetSchema(AccountsSchema.Name);
            builder.Entity<BankAccountTypeEntity>().HasKey(x => x.BankAccountTypeID);
            builder.Entity<BankAccountTypeEntity>().Property(x => x.BankAccountTypeID).UseIdentityColumn();
            builder.Entity<BankAccountTypeEntity>().Property(x => x.Description).IsRequired().IsUnicode().HasMaxLength(BankAccountType.DescriptionMaxLength);
            builder.Entity<BankAccountTypeEntity>().Property(x => x.AllowPersonal).IsRequired().HasDefaultValue(false);
            builder.Entity<BankAccountTypeEntity>().Property(x => x.AllowCompany).IsRequired().HasDefaultValue(false);
            builder.Entity<BankAccountTypeEntity>().Property(x => x.IsDeprecated).IsRequired().HasDefaultValue(false);

            builder.Entity<BankAccountTypeEntity>().HasData(new BankAccountTypeEntity[]
            {
                    new BankAccountTypeEntity() { BankAccountTypeID = 1, Description = "Current Account", AllowCompany = true, AllowPersonal = true },
                    new BankAccountTypeEntity() { BankAccountTypeID = 2, Description = "Savings Account", AllowCompany = true, AllowPersonal = true },
            });
        }

        private static void Payee_OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PayeeEntity>().ToTable("Payee").Metadata.SetSchema(AccountsSchema.Name);
            builder.Entity<PayeeEntity>().HasKey(x => x.PayeeID);
            builder.Entity<PayeeEntity>().Property(x => x.Name).IsRequired().IsUnicode().HasMaxLength(Constants.NameMaxLength);
            // TODO: Payee_OnModelCreating
        }
    }
}
