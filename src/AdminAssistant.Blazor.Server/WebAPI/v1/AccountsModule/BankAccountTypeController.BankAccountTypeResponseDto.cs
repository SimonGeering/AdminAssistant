using AdminAssistant.Modules.AccountsModule;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

public sealed record BankAccountTypeResponseDto : IMapFrom<BankAccountType>
{
    public int BankAccountTypeID { get; init; }
    public string Description { get; init; } = string.Empty;
}
