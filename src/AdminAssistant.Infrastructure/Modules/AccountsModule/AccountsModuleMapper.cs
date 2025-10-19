using AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;
using AdminAssistant.Modules.CoreModule;

namespace AdminAssistant.Modules.AccountsModule;

public static class AccountsModuleMapper
{
    public static List<BankAccountTransaction> ToBankAccountTransactionList(this List<BankAccountTransactionEntity> entities)
        => entities.Select(ToBankAccountTransaction).ToList();

    public static BankAccountTransaction ToBankAccountTransaction(this BankAccountTransactionEntity entity)
        => new()
        {
            BankAccountTransactionID = new BankAccountTransactionId(entity.BankAccountTransactionID),
            BankAccountID = new BankAccountId(entity.BankAccountID),
            BankAccountTransactionTypeID = new BankAccountTransactionTypeId(entity.BankAccountTransactionTypeID),
            BankAccountStatementID = new BankAccountStatementId(entity.BankAccountStatementID),
            BankAccountStatementNumber = entity.BankAccountStatementNumber,
            IsReconciled = entity.IsReconciled,
            PayeeID = new PayeeId(entity.PayeeID),
            CurrencyID = new CurrencyId(entity.CurrencyID),
            Credit = entity.Credit,
            Debit = entity.Debit,
            Description = entity.Description,
            Notes = entity.Notes,
            TransactionDate = entity.TransactionDate
        };

    public static BankAccountEntity ToBankAccountEntity(this BankAccount domainObject)
        => new()
        {
            BankAccountID = domainObject.BankAccountID.Value,
            BankAccountTypeID = domainObject.BankAccountTypeID.Value,
            CurrencyID = domainObject.CurrencyID.Value,
            AccountName = domainObject.AccountName,
            OpeningBalance = domainObject.OpeningBalance,
            CurrentBalance = domainObject.CurrentBalance,
            OpenedOn = domainObject.OpenedOn,
            IsBudgeted = domainObject.IsBudgeted,
        };

    public static List<BankAccountEntity> ToBankAccountEntityList(this List<BankAccount> domainObjects)
        => domainObjects.Select(ToBankAccountEntity).ToList();

    public static BankAccount ToBankAccount(this BankAccountEntity entity)
        => new()
        {
            BankAccountID = new BankAccountId(entity.BankAccountID),
            BankAccountTypeID = new BankAccountTypeId(entity.BankAccountTypeID),
            CurrencyID = new CurrencyId(entity.CurrencyID),
            OwnerID = entity.OwnerID,
            AccountName = entity.AccountName,
            IsBudgeted = entity.IsBudgeted,
            OpeningBalance = entity.OpeningBalance,
            CurrentBalance = entity.CurrentBalance,
            OpenedOn = entity.OpenedOn,
        };

    public static List<BankAccount> ToBankAccountList(this List<BankAccountEntity> entities)
        => entities.Select(ToBankAccount).ToList();

    public static List<BankAccountTypeEntity> ToBankAccountTypeEntityList(this List<BankAccountType> domainObjects)
        => domainObjects.Select(ToBankAccountTypeEntity).ToList();

    public static BankAccountTypeEntity ToBankAccountTypeEntity(this BankAccountType domainObject)
        => new()
        {
            BankAccountTypeID = domainObject.BankAccountTypeID.Value,
            Description = domainObject.Description
        };

    public static BankAccountType ToBankAccountType(this BankAccountTypeEntity entity)
        => new()
        {
            BankAccountTypeID = new BankAccountTypeId(entity.BankAccountTypeID),
            Description = entity.Description,
        };

    public static List<BankAccountType> ToBankAccountTypeList(this List<BankAccountTypeEntity> entities)
        => entities.Select(ToBankAccountType).ToList();

    public static Bank ToBank(this BankEntity entity)
        => new()
        {
            BankID = new BankId(entity.BankID),
            BankName = new BankName(entity.BankName),
        };

    public static List<Bank> ToBankList(this List<BankEntity> entities)
        => entities.Select(ToBank).ToList();

    public static BankEntity ToBankEntity(this Bank domainObject)
        => new()
        {
            BankID = domainObject.BankID.Value,
            BankName = domainObject.BankName.Value,
        };
}
