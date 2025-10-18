namespace AdminAssistant.Infrastructure.EntityFramework.Model.Core;

public sealed class CurrencyEntity
{
    public int CurrencyID { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string DecimalFormat { get; set; } = string.Empty;
    public bool IsDeprecated { get; set; }
}
