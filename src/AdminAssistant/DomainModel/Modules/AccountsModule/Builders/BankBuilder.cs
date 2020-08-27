namespace AdminAssistant.DomainModel.Modules.AccountsModule.Builders
{
    public interface IBankBuilder
    {
        Bank Build();
        IBankBuilder WithTestData(int bankID = Constants.UnknownRecordID);
        IBankBuilder WithBankName(string bankName);
    }
    internal class BankBuilder : Bank, IBankBuilder
    {
        public Bank Build() => this;

        public IBankBuilder WithTestData(int bankID = Constants.UnknownRecordID)
        {
            BankID = bankID;
            BankName = "ACME Bank PLC";
            return this;
        }

        public IBankBuilder WithBankName(string bankName)
        {
            BankName = bankName;
            return this;
        }
    }
}
