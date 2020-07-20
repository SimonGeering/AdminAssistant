namespace AdminAssistant.DomainModel.Modules.AccountsModule.TestDataBuilders
{
    public interface ICurrencyTestDataBuilder
    {
        Currency Build();
        ICurrencyTestDataBuilder WithoutADecimalFormat();
        ICurrencyTestDataBuilder WithDecimalFormat(string decimalFormat);
        ICurrencyTestDataBuilder WithoutASymbol();
        ICurrencyTestDataBuilder WithSymbol(string symbol);
        ICurrencyTestDataBuilder WithTestData(int currencyID = Constants.UnknownRecordID);
    }
    public class CurrencyTestDataBuilder : Currency, ICurrencyTestDataBuilder
    {
        public static Currency Default(ICurrencyTestDataBuilder builder) => builder.Build();

        public Currency Build()
        {
            return this;
        }

        public ICurrencyTestDataBuilder WithoutADecimalFormat() => this.WithDecimalFormat(string.Empty);
        public ICurrencyTestDataBuilder WithDecimalFormat(string decimalFormat)
        {
            this.DecimalFormat = decimalFormat;
            return this;
        }

        public ICurrencyTestDataBuilder WithoutASymbol() => this.WithSymbol(string.Empty);
        public ICurrencyTestDataBuilder WithSymbol(string symbol)
        {
            this.Symbol = symbol;
            return this;
        }

        public ICurrencyTestDataBuilder WithTestData(int currencyID = Constants.UnknownRecordID)
        {
            this.CurrencyID = currencyID;
            this.Symbol = "GBP";
            this.DecimalFormat = "2.2-2";

            return this;
        }
    }
}
