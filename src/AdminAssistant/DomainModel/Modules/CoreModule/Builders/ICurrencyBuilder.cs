namespace AdminAssistant.DomainModel.Modules.CoreModule.Builders;

public interface ICurrencyBuilder
{
    Currency Build();
    ICurrencyBuilder WithoutADecimalFormat();
    ICurrencyBuilder WithDecimalFormat(string decimalFormat);
    ICurrencyBuilder WithoutASymbol();
    ICurrencyBuilder WithSymbol(string symbol);
    ICurrencyBuilder WithTestData(int currencyID = Constants.UnknownRecordID);
}
