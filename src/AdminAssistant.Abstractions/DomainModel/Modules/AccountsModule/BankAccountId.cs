namespace AdminAssistant.DomainModel.Modules.AccountsModule;

public sealed record BankAccountId(int Value) : Id(Value)
{
    public static BankAccountId Default => new(Constants.UnknownRecordID);
}


