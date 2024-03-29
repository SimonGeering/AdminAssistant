using AdminAssistant.Modules.AccountsModule;

namespace AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;

public sealed class BankAccountTypeEntity : IMapFrom<BankAccountType>, IMapTo<BankAccountType>
{
    public int BankAccountTypeID { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool AllowPersonal { get; set; }
    public bool AllowCompany { get; set; }
    public bool IsDeprecated { get; set; }

    public void MapFrom(AutoMapper.Profile profile) => profile
        .CreateMap<BankAccountType, BankAccountTypeEntity>()
        .ForMember(x => x.AllowPersonal, opt => opt.Ignore())
        .ForMember(x => x.AllowCompany, opt => opt.Ignore())
        .ForMember(x => x.IsDeprecated, opt => opt.Ignore());
}
