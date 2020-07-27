namespace AdminAssistant.DomainModel.Modules.AccountsModule
{
    public class BankAccountType
    {
        public const int DescriptionMaxLength = Constants.DescriptionMaxLength;

        public int BankAccountTypeID { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
