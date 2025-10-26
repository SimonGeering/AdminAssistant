using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.CoreModule;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

public static class AccountsModuleMapper
{
    public static BankAccountCreateRequestDto ToBankAccountCreateRequestDto(this BankAccount source)
        => new()
        {
            BankAccountTypeID = source.BankAccountTypeID.Value,
            CurrencyID = source.CurrencyID.Value,
            // TODO: OwnerID =
            AccountName = source.AccountName,
            IsBudgeted = source.IsBudgeted,
            OpeningBalance = source.OpeningBalance,
            CurrentBalance = source.CurrentBalance,
            OpenedOn = source.OpenedOn,
        };

    public static BankCreateRequestDto ToBankCreateRequestDto(this Bank source)
        => new()
        {
            BankName = source.BankName.Value
        };

    public static BankUpdateRequestDto ToBankUpdateRequestDto(this Bank source)
        => new()
        {
            BankID = source.BankID.Value,
            BankName = source.BankName.Value
        };

    public static BankAccountUpdateRequestDto ToBankAccountUpdateRequestDto(this BankAccount source)
        => new()
        {
            BankAccountID = source.BankAccountID.Value,
            BankAccountTypeID = source.BankAccountTypeID.Value,
            CurrencyID = source.CurrencyID.Value,
            // TODO: OwnerID =
            AccountName = source.AccountName,
            IsBudgeted = source.IsBudgeted,
            OpeningBalance = source.OpeningBalance,
            CurrentBalance = source.CurrentBalance,
            OpenedOn = source.OpenedOn,
        };

    public static BankAccount ToBankAccount(this BankAccountCreateRequestDto source)
        => new()
        {
            BankAccountID = BankAccountId.Default,
            BankAccountTypeID = BankAccountTypeId.Default,
            CurrencyID = CurrencyId.Default,
            // TODO: OwnerID =
            AccountName = source.AccountName,
            IsBudgeted = source.IsBudgeted,
            OpeningBalance = source.OpeningBalance,
            CurrentBalance = source.CurrentBalance,
            OpenedOn = source.OpenedOn,
        };

    public static BankAccount ToBankAccount(this BankAccountUpdateRequestDto source)
        => new()
        {
            BankAccountID = new BankAccountId(source.BankAccountID),
            BankAccountTypeID = new BankAccountTypeId(source.BankAccountTypeID),
            CurrencyID = new CurrencyId(source.CurrencyID),
            // TODO: OwnerID =
            AccountName = source.AccountName,
            IsBudgeted = source.IsBudgeted,
            OpeningBalance = source.OpeningBalance,
            CurrentBalance = source.CurrentBalance,
            OpenedOn = source.OpenedOn,
        };

    public static BankAccountResponseDto ToBankAccountResponseDto(this BankAccount source)
        => new()
        {
            BankAccountID = source.BankAccountID.Value,
            BankAccountTypeID = source.BankAccountTypeID.Value,
            CurrencyID = source.CurrencyID.Value,
            AccountName = source.AccountName,
            IsBudgeted = source.IsBudgeted,
            OpeningBalance = source.OpeningBalance,
            CurrentBalance = source.CurrentBalance,
            OpenedOn = source.OpenedOn
        };

    public static IEnumerable<BankAccountTransactionResponseDto> ToBankAccountTransactionResponseDtoEnumeration(this IEnumerable<BankAccountTransaction> source)
        => source.Select(x => new BankAccountTransactionResponseDto
        {
            BankAccountTransactionID = x.BankAccountTransactionID.Value,
            BankAccountID = x.BankAccountID.Value,
            BankAccountTransactionTypeID = x.BankAccountTransactionTypeID.Value,
            BankAccountStatementID = x.BankAccountStatementID.Value,
            BankAccountStatementNumber = x.BankAccountStatementNumber,
            IsReconciled = x.IsReconciled,
            PayeeID = x.PayeeID.Value,
            PayeeName = x.PayeeName,
            CurrencyID = x.CurrencyID.Value,
            Symbol = x.Symbol,
            DecimalFormat = x.DecimalFormat,
            Credit = x.Credit,
            Debit = x.Debit,
            Balance = x.Balance,
            Description = x.Description,
            Notes = x.Notes,
            TransactionDate = x.TransactionDate.ToString(Constants.DateFormat)
        });

    public static IEnumerable<BankAccountInfoResponseDto> ToBankAccountInfoResponseDtoEnumeration(this IEnumerable<BankAccountInfo> source)
        => source.Select(x => new BankAccountInfoResponseDto
        {
            BankAccountID = x.BankAccountID.Value,
            AccountName = x.AccountName,
            CurrentBalance = x.CurrentBalance,
            Symbol = x.Symbol,
            DecimalFormat = x.DecimalFormat,
            IsBudgeted = x.IsBudgeted
        });

    public static IEnumerable<BankAccountTypeResponseDto> ToBankAccountTypeResponseDtoEnumeration(this IEnumerable<BankAccountType> source)
        => source.Select(x => new BankAccountTypeResponseDto
        {
            BankAccountTypeID = x.BankAccountTypeID.Value,
            Description = x.Description
        });

    public static Bank ToBank(this BankCreateRequestDto source)
        => new()
        {
            BankID = BankId.Default,
            BankName = new BankName(source.BankName)
        };

    public static Bank ToBank(this BankUpdateRequestDto source)
        => new()
        {
            BankID = new BankId(source.BankID),
            BankName = new BankName(source.BankName)
        };

    public static BankResponseDto ToBankResponseDto(this Bank source)
        => new()
        {
            BankID = source.BankID.Value,
            BankName = source.BankName.Value
        };

    public static IEnumerable<BankResponseDto> ToBankResponseDtoEnumeration(this IEnumerable<Bank> source)
        => source.Select(x => new BankResponseDto
        {
            BankID = x.BankID.Value,
            BankName = x.BankName.Value
        });
}
