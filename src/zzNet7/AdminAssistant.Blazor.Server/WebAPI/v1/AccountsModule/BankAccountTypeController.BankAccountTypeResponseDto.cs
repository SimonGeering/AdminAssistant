using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

public sealed record BankAccountTypeResponseDto : IMapFrom<BankAccountType>
{
    public int BankAccountTypeID { get; init; }
    public string Description { get; init; } = string.Empty;
}
