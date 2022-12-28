using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.UI.Modules.AccountsModule;

public sealed class EditBankAccountMessage
{
    public EditBankAccountMessage(BankAccount bankAccount) => BankAccount = bankAccount;

    public BankAccount BankAccount { get; set; }
}
