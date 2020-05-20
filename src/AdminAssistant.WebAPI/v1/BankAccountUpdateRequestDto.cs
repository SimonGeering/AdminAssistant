using System;
using AdminAssistant.DomainModel.Modules.Accounts;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1
{
    public class BankAccountUpdateRequestDto : IMapTo<BankAccount>
    {
        public int BankAccountID { get; set; }
        public int BankAccountTypeID { get; set; }
        public int CurrencyID { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public int Balance { get; set; }
        public bool IsBudgeted { get; set; }
        public int OpeningBalance { get; set; }
        public int CurrentBalance { get; set; }
        public DateTime OpenedOn { get; set; }
    }
}
