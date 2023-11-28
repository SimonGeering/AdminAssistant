namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public sealed record BankId(int Value) : Id(Value)
{
    public static BankId Default => new(Constants.UnknownRecordID);
}
