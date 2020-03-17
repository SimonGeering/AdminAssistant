namespace AdminAssistant.DomainModel.Modules.Accounts
{
    public class Currency
    {
        public const int SymbolMaxLength = 3;
        public const int DecimalFormatMaxLength = 5;

        public int CurrencyID { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string DecimalFormat { get; set; } = string.Empty;
    }
}
