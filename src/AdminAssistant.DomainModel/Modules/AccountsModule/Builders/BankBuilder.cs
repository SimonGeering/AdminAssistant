namespace AdminAssistant.Modules.AccountsModule.Builders;

public interface IBankBuilder
{
    Bank Build();
    IBankBuilder WithTestData(int bankID = Constants.UnknownRecordID);
    IBankBuilder WithBankName(string bankName);
}
internal sealed class BankBuilder : IBankBuilder
{
    private Bank _bank = new();

    public static Bank Default(IBankBuilder builder) => builder.Build();
    public Bank Build() => _bank;

    public IBankBuilder WithTestData(int bankID = Constants.UnknownRecordID)
    {
        _bank = _bank with
        {
            BankID = new(bankID),
            BankName = new("ACME Bank PLC")
        };
        return this;
    }

    public IBankBuilder WithBankName(string bankName)
    {
        _bank = _bank with { BankName = new(bankName) };
        return this;
    }
}
