using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;

public sealed class BankAccountTransactionTypeEntity : IMapFrom<BankAccountTransactionType>, IMapTo<BankAccountTransactionType>
{
    public int BankAccountTransactionTypeID { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool AllowPersonal { get; set; }
    public bool AllowCompany { get; set; }
    public bool IsDeprecated { get; set; }

    public void MapFrom(AutoMapper.Profile profile) => profile
        .CreateMap<BankAccountTransactionType, BankAccountTransactionTypeEntity>()
        .ForMember(x => x.AllowPersonal, opt => opt.Ignore())
        .ForMember(x => x.AllowCompany, opt => opt.Ignore())
        .ForMember(x => x.IsDeprecated, opt => opt.Ignore());
}
