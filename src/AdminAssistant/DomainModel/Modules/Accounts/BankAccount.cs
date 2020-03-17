using System;

namespace AdminAssistant.DomainModel.Modules.Accounts
{
    public class BankAccount
    {
        public const int AccountNameMaxLength = Constants.NameMaxLength;

        public int BankAccountID { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public Currency Currency { get; set; } = new Currency();
        public int Balance { get; set; }
        public bool IsBudgeted { get; set; }

        public BankAccountType AccountType { get; set; } = new BankAccountType();
        public int OpeningBalance { get; set; }
        public int CurrentBalance { get; set; }
        public DateTime OpenedOn { get; set; }
    }
}
