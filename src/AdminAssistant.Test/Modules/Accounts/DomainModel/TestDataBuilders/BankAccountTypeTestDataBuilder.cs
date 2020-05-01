using System;
using System.Collections.Generic;
using System.Text;

namespace AdminAssistant.DomainModel.Modules.Accounts.TestDataBuilders
{
    public interface IBankAccountTypeTestDataBuilder
    {
        BankAccountType Build();
        IBankAccountTypeTestDataBuilder WithTestData(int bankAccountTypeID = Constants.NewRecordID);
        IBankAccountTypeTestDataBuilder WithDescription(string description);
    }
    public class BankAccountTypeTestDataBuilder : BankAccountType, IBankAccountTypeTestDataBuilder
    {
        public static BankAccountType Default(IBankAccountTypeTestDataBuilder builder) => builder.Build();

        public BankAccountType Build()
        {
            return this;
        }

        public IBankAccountTypeTestDataBuilder WithTestData(int bankAccountTypeID = Constants.NewRecordID)
        {
            this.BankAccountTypeID = bankAccountTypeID;
            this.Description = "A valid BankAccountType description";
            return this;
        }

        public IBankAccountTypeTestDataBuilder WithDescription(string description)
        {
            this.Description = description;
            return this;
        }
    }
}
