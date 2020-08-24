using System;
using AdminAssistant.Infra.DAL;

namespace AdminAssistant.DomainModel.Modules.AccountsModule
{
    public class BankAccountTransaction : IDatabasePersistable
    {
        public const int DescriptionMaxLength = Constants.DescriptionMaxLength;

        public int BankAccountTransactionID { get; set; }
        public int BankAccountID { get; set; }

        public int PayeeID { get; set; }
        public string PayeeName { get; set; } = string.Empty;

        public int CurrencyID { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string DecimalFormat { get; set; } = string.Empty;

        public int Credit { get; set; }
        public int Debit { get; set; }
        public int Balance { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public int PrimaryKey => this.BankAccountTransactionID;
    }
}
