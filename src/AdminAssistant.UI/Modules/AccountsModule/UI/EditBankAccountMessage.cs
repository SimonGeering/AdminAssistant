namespace AdminAssistant.Modules.AccountsModule.UI;

public sealed class EditBankAccountMessage(BankAccount bankAccount)
{
    public BankAccount BankAccount { get; set; } = bankAccount;
}
