using System;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1
{
    public class BankAccountCreateRequestDto : IMapTo<BankAccount>
    {
        public int BankAccountTypeID { get; set; }
        public int CurrencyID { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public int Balance { get; set; }
        public bool IsBudgeted { get; set; }
        public int OpeningBalance { get; set; }
        public int CurrentBalance { get; set; }
        public DateTime OpenedOn { get; set; }

        public void MapTo(AutoMapper.Profile profile)
        {
            profile.CreateMap< BankAccountCreateRequestDto, BankAccount>()
                .ForMember(x => x.BankAccountID, opt => opt.Ignore());
        }
    }
}
