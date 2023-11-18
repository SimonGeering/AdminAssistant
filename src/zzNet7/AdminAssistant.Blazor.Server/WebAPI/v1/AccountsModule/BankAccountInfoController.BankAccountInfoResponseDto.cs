using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

public sealed record BankAccountInfoResponseDto : IMapFrom<BankAccountInfo>
{
    public int BankAccountID { get; init; }
    public string AccountName { get; init; } = string.Empty;
    public int CurrentBalance { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;
    public bool IsBudgeted { get; init; }
}
