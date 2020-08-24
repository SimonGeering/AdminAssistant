using AdminAssistant.Infra.DAL;

namespace AdminAssistant.DomainModel.Modules.AccountsModule
{
    public class Bank : IDatabasePersistable
    {
        public const int BankNameMaxLength = Constants.NameMaxLength;

        public int BankID { get; set; } = Constants.UnknownRecordID;
        public string BankName { get; set; } = string.Empty;

        public int PrimaryKey => this.BankID;
    }
}
