namespace AdminAssistant.DomainModel.Modules.CoreModule;

public sealed record Currency : IDatabasePersistable
{
    public const int SymbolMaxLength = 3;
    public const int DecimalFormatMaxLength = 5;

    public CurrencyId CurrencyID { get; set; } = CurrencyId.Default;
    public string Symbol { get; set; } = string.Empty;
    public string DecimalFormat { get; set; } = string.Empty;

    public int PrimaryKey => CurrencyID.Value;
}
