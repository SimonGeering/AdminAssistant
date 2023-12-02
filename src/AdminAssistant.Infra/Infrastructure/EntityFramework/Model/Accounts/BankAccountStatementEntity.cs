using AdminAssistant.Modules.AccountsModule;

namespace AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;

public sealed class BankAccountStatementEntity : IMapFrom<BankAccountStatement>, IMapTo<BankAccountStatement>
{
    // Table "Accounts.BankAccountStatement"
    public int BankAccountStatementID { get; set; } // PK
    public int BankAccountID { get; set; }
    public int DocumentID { get; set; }
    public int AuditID { get; set; }
    public int OwnerID { get; internal set; }

    public DateTime StatementDate { get; set; }
    public bool IsReconciled { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int OpeningBalance { get; set; }
    public int ClosingBalance { get; set; }
    public int TotalPaymentsIn { get; set; }
    public int TotalPaymentsOut { get; set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
    public Core.OwnerEntity Owner { get; internal set; } = null!;

    public void MapFrom(AutoMapper.Profile profile) => profile
        .CreateMap<BankAccountStatement, BankAccountStatementEntity>()
        .ForMember(x => x.AuditID, opt => opt.Ignore())
        .ForMember(x => x.Audit, opt => opt.Ignore())
        .ForMember(x => x.OwnerID, opt => opt.Ignore())
        .ForMember(x => x.Owner, opt => opt.Ignore());
}
