using AdminAssistant.DomainModel.Modules.AccountsModule;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace AdminAssistant.UI.Modules.AccountsModule
{
    public class EditBankAccountMessage
    {
        public EditBankAccountMessage(BankAccount bankAccount)
        {
            this.BankAccount = bankAccount;
        }

        public BankAccount BankAccount { get; set; }
    }
}
