using System;

namespace AdminAssistant.DomainModel.Modules.AccountsModule
{
    public class BankAccount : IDatabasePersistable
    {
        public const int AccountNameMaxLength = Constants.NameMaxLength;

        public int BankAccountID { get; set; }
        public int BankAccountTypeID { get; set; } = Constants.UnknownRecordID;
        public int CurrencyID { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public int Balance { get; set; }
        public bool IsBudgeted { get; set; }
        public int OpeningBalance { get; set; }
        public int CurrentBalance { get; set; }
        public DateTime OpenedOn { get; set; }

        public int PrimaryKey => this.BankAccountID;
    }
}
