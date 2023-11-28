namespace AdminAssistant.DomainModel.Modules.CoreModule;

public sealed record CurrencyId(int Value) : Id(Value)
{
    public static CurrencyId Default => new(Constants.UnknownRecordID);
}
