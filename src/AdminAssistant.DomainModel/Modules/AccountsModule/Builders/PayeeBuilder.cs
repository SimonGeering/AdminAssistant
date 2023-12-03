namespace AdminAssistant.Modules.AccountsModule.Builders;

public interface IPayeeBuilder
{
    Payee Build();
    IPayeeBuilder WithTestData(int payeeID = Constants.UnknownRecordID);
    IPayeeBuilder WithName(string name);
}
internal sealed class PayeeBuilder : IPayeeBuilder
{
    private Payee _payee = new();

    public Payee Build() => _payee;

    public IPayeeBuilder WithTestData(int payeeID = Constants.UnknownRecordID)
    {
        _payee = _payee with
        {
            PayeeID = new(payeeID),
            Name = $"Payee {payeeID}",
        };

        return this;
    }

    public IPayeeBuilder WithName(string name)
    {
        _payee = _payee with { Name = name };
        return this;
    }
}
