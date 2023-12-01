namespace AdminAssistant.Modules.CoreModule;

public sealed record Currency : IPersistable
{
    public const int SymbolMaxLength = 3;
    public const int DecimalFormatMaxLength = 5;

    public CurrencyId CurrencyID { get; set; } = CurrencyId.Default;
    public string Symbol { get; set; } = string.Empty;
    public string DecimalFormat { get; set; } = string.Empty;

    public Id PrimaryKey => CurrencyID;
}
public sealed record CurrencyId(int Value) : Id(Value)
{
    public static CurrencyId Default => new(Constants.UnknownRecordID);
}
