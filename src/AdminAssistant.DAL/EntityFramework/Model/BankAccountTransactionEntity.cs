using System;
using AdminAssistant.Accounts.DomainModel;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.Accounts.DAL.EntityFramework.Model
{
    public class BankAccountTransactionEntity : IMapping<BankAccountTransactionEntity, BankAccountTransaction>
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

        public AuditEntity Audit { get; internal set; } = null!;
    }
}
