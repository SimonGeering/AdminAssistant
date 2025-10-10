using AdminAssistant.Modules.AccountsModule;

namespace AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;

public sealed class BankAccountTransactionEntity : IMapFrom<BankAccountTransaction>, IMapTo<BankAccountTransaction>
{
    // Table "Accounts.BankAccountTransaction"
    public int BankAccountTransactionID { get; set; } // PK
    public int BankAccountID { get; set; }
    public int AuditID { get; internal set; }
    public int PayeeID { get; internal set; }
    public int CurrencyID { get; internal set; }
    public int BankAccountTransactionTypeID { get; internal set; }
    public int BankAccountStatementID { get; internal set; }
    public int BankAccountStatementNumber { get; internal set; }
    public bool IsReconciled { get; internal set; }
    public DateTime TransactionDate { get; internal set; }
    public int Credit { get; internal set; }
    public int Debit { get; internal set; }
    public string Description { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

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
