namespace AdminAssistant.Modules.AccountsModule.UI;

public sealed class EditBankAccountMessage
{
    public EditBankAccountMessage(BankAccount bankAccount) => BankAccount = bankAccount;

    public BankAccount BankAccount { get; set; }
}
