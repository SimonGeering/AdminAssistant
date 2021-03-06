namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders
{
    public interface IBankBuilder
    {
        Bank Build();
        IBankBuilder WithTestData(int bankID = Constants.UnknownRecordID);
        IBankBuilder WithBankName(string bankName);
    }
    internal class BankBuilder : IBankBuilder
    {
        private Bank _bank = new Bank();

        public static Bank Default(IBankBuilder builder) => builder.Build();
        public Bank Build() => _bank;

        public IBankBuilder WithTestData(int bankID = Constants.UnknownRecordID)
        {
            _bank = _bank with
            {
                BankID = bankID,
                BankName = "ACME Bank PLC"
            };
            return this;
        }

        public IBankBuilder WithBankName(string bankName)
        {
            _bank = _bank with { BankName = bankName };
            return this;
        }
    }
}
