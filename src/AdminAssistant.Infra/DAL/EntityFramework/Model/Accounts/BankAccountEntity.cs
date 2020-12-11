using System;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts
{
    public class BankAccountEntity : IMapFrom<BankAccount>, IMapTo<BankAccount>
    {
        public int BankAccountID { get; set; }
        public int AuditID { get; set; }
        public int OwnerID { get; internal set; }
        public int BankAccountTypeID { get; set; }
        public int CurrencyID { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public int OpeningBalance { get; set; }
        public int CurrentBalance { get; set; }
        public DateTime OpenedOn { get; set; }
        public bool IsBudgeted { get; set; }
        public Core.AuditEntity Audit { get; internal set; } = null!;
        public Core.OwnerEntity Owner { get; internal set; } = null!;
        public Core.CurrencyEntity Currency { get; internal set; } = null!;

        public void MapFrom(AutoMapper.Profile profile) => profile
            .CreateMap<BankAccount, BankAccountEntity>()
            .ForMember(x => x.AuditID, opt => opt.Ignore())
            .ForMember(x => x.Audit, opt => opt.Ignore())
            .ForMember(x => x.OwnerID, opt => opt.Ignore())
            .ForMember(x => x.Owner, opt => opt.Ignore())
            .ForMember(x => x.Currency, opt => opt.Ignore());
    }
}
