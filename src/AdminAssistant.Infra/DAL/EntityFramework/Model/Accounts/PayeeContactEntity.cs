using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;

public sealed class PayeeContactEntity // : IMapFrom<PayeeContact>, IMapTo<PayeeContact>
{
    // Table "Accounts.PayeeContact"
    public int PayeeContactID { get; set; } // PK
    public int PayeeID { get; set; }
    public int ContactID { get; set; }
    public int AuditID { get; set; }
    public bool IsPrimaryContact { get; set; }

    public void MapFrom(AutoMapper.Profile profile) => profile
        .CreateMap<BankAccountType, BankAccountTypeEntity>()
        .ForMember(x => x.AllowPersonal, opt => opt.Ignore())
        .ForMember(x => x.AllowCompany, opt => opt.Ignore())
        .ForMember(x => x.IsDeprecated, opt => opt.Ignore());
}
