using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;

public class BankAccountTransactionEntity : IMapFrom<BankAccountTransaction>, IMapTo<BankAccountTransaction>
{
    public int BankAccountTransactionID { get; set; }
    public int BankAccountID { get; set; }
    public int AuditID { get; internal set; }
    public int PayeeID { get; internal set; }
    public int CurrencyID { get; internal set; }
    public int Credit { get; internal set; }
    public int Debit { get; internal set; }
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;

    public void MapFrom(AutoMapper.Profile profile) => profile
        .CreateMap<BankAccountTransaction, BankAccountTransactionEntity>()
        .ForMember(x => x.AuditID, opt => opt.Ignore())
        .ForMember(x => x.Audit, opt => opt.Ignore());

    // TODO: Resolve mapping of properties from BankAccountTransactionEntity to BankAccountTransaction
    public void MapTo(AutoMapper.Profile profile) => profile
        .CreateMap<BankAccountTransactionEntity, BankAccountTransaction>()
        .ForMember(x => x.PayeeName, opt => opt.Ignore())
        .ForMember(x => x.Symbol, opt => opt.Ignore())
        .ForMember(x => x.DecimalFormat, opt => opt.Ignore())
        .ForMember(x => x.Balance, opt => opt.Ignore());
}
