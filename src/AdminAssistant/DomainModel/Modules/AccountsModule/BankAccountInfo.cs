using AdminAssistant.Infra.DAL;

namespace AdminAssistant.DomainModel.Modules.AccountsModule
{
    public class BankAccountInfo : IDatabasePersistable
    {
        public int BankAccountID { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public int CurrentBalance { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string DecimalFormat { get; set; } = string.Empty;
        public bool IsBudgeted { get; set; }

        public int PrimaryKey => this.BankAccountID;
    }
}
