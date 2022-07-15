using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.AccountsModule
{
    [SwaggerSchema(Required = new[] { "BankAccountID", "BankAccountTypeID", "CurrencyID", "AccountName", "IsBudgeted", "OpeningBalance", "OpenedOn" })]
    public record BankAccountUpdateRequestDto : IMapTo<BankAccount>
    {
        [SwaggerSchema("The BankAccount identifier.", ReadOnly = true)]
        public int BankAccountID { get; init; }
        [SwaggerSchema("The BankAccountType for this BankAccount.")]
        public int BankAccountTypeID { get; init; }
        public int CurrencyID { get; init; }
        public string AccountName { get; init; } = string.Empty;
        public bool IsBudgeted { get; init; }
        public int OpeningBalance { get; init; }
        public int CurrentBalance { get; init; }
        public DateTime OpenedOn { get; init; }

        public void MapTo(AutoMapper.Profile profile)
            => profile.CreateMap<BankAccountUpdateRequestDto, BankAccount>()
                      .ForMember(x => x.OwnerID, opt => opt.Ignore());
    }
}
