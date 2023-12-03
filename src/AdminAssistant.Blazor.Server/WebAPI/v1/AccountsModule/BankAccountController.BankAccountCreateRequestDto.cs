using AdminAssistant.Modules.AccountsModule;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

[SwaggerSchema(Required = new[] { "BankAccountTypeID", "CurrencyID", "AccountName", "IsBudgeted", "OpeningBalance", "OpenedOn" })]
public sealed record BankAccountCreateRequestDto : IMapTo<BankAccount>
{
    [SwaggerSchema("The BankAccountType for this BankAccount.")]
    public int BankAccountTypeID { get; init; }
    public int CurrencyID { get; init; }
    public string AccountName { get; init; } = string.Empty;
    public int Balance { get; init; }
    public bool IsBudgeted { get; init; }
    public int OpeningBalance { get; init; }
    public int CurrentBalance { get; init; }
    public DateTime OpenedOn { get; init; }

    public void MapTo(AutoMapper.Profile profile)
        => profile.CreateMap<BankAccountCreateRequestDto, BankAccount>()
                  .ForMember(x => x.BankAccountID, opt => opt.Ignore())
                  .ForMember(x => x.OwnerID, opt => opt.Ignore());
}
