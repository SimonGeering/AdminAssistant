using AdminAssistant.Infra.DAL;

namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public record BankAccountType : IDatabasePersistable
{
    public const int DescriptionMaxLength = Constants.DescriptionMaxLength;

    public int BankAccountTypeID { get; init; } = Constants.UnknownRecordID;
    public string Description { get; init; } = string.Empty;
    public int PrimaryKey => BankAccountTypeID;
}
