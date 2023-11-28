using AdminAssistant.Abstractions.DomainModel.Shared;

namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public sealed record BankAccountType : IDatabasePersistable
{
    public const int DescriptionMaxLength = EntityDescription.MaxLength;

    public BankAccountTypeId BankAccountTypeID { get; init; } = BankAccountTypeId.Default;
    public string Description { get; init; } = string.Empty;
    public Id PrimaryKey => BankAccountTypeID;
}
public sealed record BankAccountTypeId(int Value) : Id(Value)
{
    public static BankAccountTypeId Default => new(Constants.UnknownRecordID);
}
