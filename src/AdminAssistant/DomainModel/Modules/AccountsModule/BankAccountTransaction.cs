using AdminAssistant.Infra.DAL;

namespace AdminAssistant.DomainModel.Modules.AccountsModule
{
    public record BankAccountTransaction : IDatabasePersistable
    {
        public const int DescriptionMaxLength = Constants.DescriptionMaxLength;

        public int BankAccountTransactionID { get; init; }
        public int BankAccountID { get; init; }

        public int PayeeID { get; init; }
        public string PayeeName { get; init; } = string.Empty;

        public int CurrencyID { get; init; }
        public string Symbol { get; init; } = string.Empty;
        public string DecimalFormat { get; init; } = string.Empty;

        public int Credit { get; init; }
        public int Debit { get; init; }
        public int Balance { get; init; }
        public string Description { get; init; } = string.Empty;
        public string Notes { get; init; } = string.Empty;
        public DateTime TransactionDate { get; init; } = DateTime.Now;

        public int PrimaryKey => BankAccountTransactionID;
    }
}
