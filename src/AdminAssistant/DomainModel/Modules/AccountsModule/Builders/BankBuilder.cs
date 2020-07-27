using System;

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
        public Bank Build()
        {
            return this;
        }

        public IBankBuilder WithTestData(int bankID = Constants.UnknownRecordID)
        {
            this.BankID = bankID;
            this.BankName = "ACME Bank PLC";
            return this;
        }

        public IBankBuilder WithBankName(string bankName)
        {
            this.BankName = bankName;
            return this;
        }
    }
}
