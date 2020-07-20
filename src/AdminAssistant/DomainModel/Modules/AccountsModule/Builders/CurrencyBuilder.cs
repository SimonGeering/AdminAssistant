namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders
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
    internal class CurrencyBuilder : Currency, ICurrencyBuilder
    {
        public static Currency Default(ICurrencyBuilder builder) => builder.Build();

        public Currency Build()
        {
            return this;
        }

        public ICurrencyBuilder WithoutADecimalFormat() => this.WithDecimalFormat(string.Empty);
        public ICurrencyBuilder WithDecimalFormat(string decimalFormat)
        {
            this.DecimalFormat = decimalFormat;
            return this;
        }

        public ICurrencyBuilder WithoutASymbol() => this.WithSymbol(string.Empty);
        public ICurrencyBuilder WithSymbol(string symbol)
        {
            this.Symbol = symbol;
            return this;
        }

        public ICurrencyBuilder WithTestData(int currencyID = Constants.UnknownRecordID)
        {
            this.CurrencyID = currencyID;
            this.Symbol = "GBP";
            this.DecimalFormat = "2.2-2";

            return this;
        }
    }
}
