using System;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1
{
    [SwaggerSchema(Required = new[] { "BankAccountTypeID", "CurrencyID", "AccountName", "IsBudgeted", "OpeningBalance", "OpenedOn" })]

    public class BankAccountCreateRequestDto : IMapTo<BankAccount>
    {
        [SwaggerSchema("The BankAccount identifier.", ReadOnly = true)]
        public int BankAccountTypeID { get; set; }
        [SwaggerSchema("The BankAccountType for this BankAccount.")]
        public int CurrencyID { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public int Balance { get; set; }
        public bool IsBudgeted { get; set; }
        public int OpeningBalance { get; set; }
        public int CurrentBalance { get; set; }
        public DateTime OpenedOn { get; set; }

        public void MapTo(AutoMapper.Profile profile)
            => profile.CreateMap<BankAccountCreateRequestDto, BankAccount>()
                      .ForMember(x => x.BankAccountID, opt => opt.Ignore());
    }
}
