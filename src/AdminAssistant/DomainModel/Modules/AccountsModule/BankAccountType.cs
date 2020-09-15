using AdminAssistant.Infra.DAL;

namespace AdminAssistant.DomainModel.Modules.AccountsModule
{
    public class BankAccountType : IDatabasePersistable
    {
        public const int DescriptionMaxLength = Constants.DescriptionMaxLength;

        public int BankAccountTypeID { get; set; }
        public string Description { get; set; } = string.Empty;
        public int PrimaryKey => BankAccountTypeID;
    }
}
