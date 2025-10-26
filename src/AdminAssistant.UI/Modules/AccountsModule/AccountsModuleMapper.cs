using AdminAssistant.Modules.CoreModule;
using AdminAssistant.WebAPI.v1.AccountsModule;

namespace AdminAssistant.Modules.AccountsModule;

internal static class AccountsModuleMapper
{
    internal static BankAccountType ToBankAccountType(this BankAccountTypeResponseDto source)
        => new()
        {
            BankAccountTypeID = new BankAccountTypeId(source.BankAccountTypeID),
            Description = source.Description,
        };

    internal static IEnumerable<BankAccountType> ToBankAccountTypeList(this IEnumerable<BankAccountTypeResponseDto> source)
        => source.Select(ToBankAccountType);

    internal static BankAccountCreateRequestDto ToBankAccountCreateRequestDto(this BankAccount source)
        => new()
        {
            AccountName = source.AccountName,
            BankAccountTypeID = source.BankAccountTypeID.Value,
            CurrencyID = source.CurrencyID.Value,
            IsBudgeted = source.IsBudgeted,
            OpeningBalance = source.OpeningBalance,
            OpenedOn = source.OpenedOn,
            CurrentBalance = source.CurrentBalance
        };

    internal static BankAccount ToBankAccount(this BankAccountResponseDto source)
        => new()
        {
            BankAccountID = new BankAccountId(source.BankAccountID),
            AccountName = source.AccountName,
            BankAccountTypeID = new BankAccountTypeId(source.BankAccountTypeID),
            CurrencyID = new CurrencyId(source.CurrencyID),
            IsBudgeted = source.IsBudgeted,
            OpeningBalance = source.OpeningBalance,
            OpenedOn = source.OpenedOn,
            CurrentBalance = source.CurrentBalance
        };

    internal static BankAccountUpdateRequestDto ToBankAccountUpdateRequestDto(this BankAccount source)
        => new()
        {
            BankAccountID = source.BankAccountID.Value,
            AccountName = source.AccountName,
            BankAccountTypeID = source.BankAccountTypeID.Value,
            CurrencyID = source.CurrencyID.Value,
            IsBudgeted = source.IsBudgeted,
            OpeningBalance = source.OpeningBalance,
            OpenedOn = source.OpenedOn,
        };
}
