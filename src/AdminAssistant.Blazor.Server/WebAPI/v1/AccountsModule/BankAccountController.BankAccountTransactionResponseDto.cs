using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

public sealed record BankAccountTransactionResponseDto : IMapFrom<BankAccountTransaction>
{
    public int BankAccountTransactionID { get; init; }
    public int BankAccountID { get; init; }
    public int BankAccountTransactionTypeID { get; init; }
    public int BankAccountStatementID { get; init; }
    public int BankAccountStatementNumber { get; init; }
    public bool IsReconciled { get; init; }

    public int PayeeID { get; init; }
    public string PayeeName { get; init; } = string.Empty;

    public int CurrencyID { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public string DecimalFormat { get; init; } = string.Empty;

    public decimal Credit { get; init; }
    public decimal Debit { get; init; }
    public decimal Balance { get; init; }
    public string Description { get; init; } = string.Empty;
    public string Notes { get; init; } = string.Empty;
    public string TransactionDate { get; init; } = string.Empty;
}
