namespace AdminAssistant.DomainModel.Modules.CoreModule.Builders
{
    public interface ICurrencyBuilder
    {
        Currency Build();
        ICurrencyBuilder WithoutADecimalFormat();
        ICurrencyBuilder WithDecimalFormat(string decimalFormat);
        ICurrencyBuilder WithoutASymbol();
        ICurrencyBuilder WithSymbol(string symbol);
        ICurrencyBuilder WithTestData(int currencyID = Constants.UnknownRecordID);
    }
    internal class CurrencyBuilder : ICurrencyBuilder
    {
        private Currency _currency = new();

        public static Currency Default(ICurrencyBuilder builder) => builder.Build();

        public Currency Build() => _currency;

        public ICurrencyBuilder WithoutADecimalFormat() => WithDecimalFormat(string.Empty);
        public ICurrencyBuilder WithDecimalFormat(string decimalFormat)
        {
            _currency = _currency with { DecimalFormat = decimalFormat };
            return this;
        }

        public ICurrencyBuilder WithoutASymbol() => WithSymbol(string.Empty);
        public ICurrencyBuilder WithSymbol(string symbol)
        {
            _currency = _currency with { Symbol = symbol };
            return this;
        }

        public ICurrencyBuilder WithTestData(int currencyID = Constants.UnknownRecordID)
        {
            _currency = _currency with
            {
                CurrencyID = currencyID,
                Symbol = "GBP",
                DecimalFormat = "2.2-2",
            };
            return this;
        }
    }
}
