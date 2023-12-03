using AdminAssistant.Modules.AccountsModule;

namespace AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;

public sealed class PayeeContactEntity : IMapFrom<PayeeContact>, IMapTo<PayeeContact>
{
    // Table "Accounts.PayeeContact"
    public int PayeeContactID { get; set; } // PK
    public int PayeeID { get; set; }
    public int ContactID { get; set; }
    public int AuditID { get; set; }
    public bool IsPrimaryContact { get; set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;

    public void MapFrom(AutoMapper.Profile profile) => profile
        .CreateMap<PayeeContact, PayeeContactEntity>()
        .ForMember(x => x.AuditID, opt => opt.Ignore())
        .ForMember(x => x.Audit, opt => opt.Ignore());
}
