namespace AdminAssistant.DomainModel.Modules.AccountsModule
{
    public class BankAccountTransaction
    {
        public int BankAccountTransactionID { get; set; }
        public int BankAccountID { get; set; }

        public int PayeeID { get; set; }
        public string PayeeName { get; set; } = string.Empty;

        public int CurrencyID { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string DecimalFormat { get; set; } = string.Empty;

        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string TransactionDate { get; set; } = string.Empty;
    }
}
