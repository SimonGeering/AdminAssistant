using AdminAssistant.Modules.CoreModule;

namespace AdminAssistant.Modules.AccountsModule;

internal static class AccountsModuleMapper
{
    internal static BankAccountType ToBankAccountType(this BankAccountTypeResponseDto source)
        => new()
        {
            BankAccountTypeID = source.BankAccountTypeID.HasValue ? new BankAccountTypeId(source.BankAccountTypeID.Value) : BankAccountTypeId.Default,
            Description = source.Description ?? string.Empty,
        };

    internal static IEnumerable<BankAccountType> ToBankAccountTypeList(this ICollection<BankAccountTypeResponseDto> source)
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
            BankAccountID = source.BankAccountID.HasValue ? new BankAccountId(source.BankAccountID.Value) : BankAccountId.Default,
            AccountName = source.AccountName ?? string.Empty,
            BankAccountTypeID = source.BankAccountTypeID.HasValue ? new BankAccountTypeId(source.BankAccountTypeID.Value) : BankAccountTypeId.Default,
            CurrencyID = source.CurrencyID.HasValue ? new CurrencyId(source.CurrencyID.Value) : CurrencyId.Default,
            IsBudgeted = source.IsBudgeted ?? false,
            OpeningBalance = source.OpeningBalance ?? 0,
            OpenedOn = source.OpenedOn ?? DateTime.MinValue,
            CurrentBalance = source.CurrentBalance ?? 0
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
