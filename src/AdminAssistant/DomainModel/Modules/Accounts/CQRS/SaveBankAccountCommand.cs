using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class SaveBankAccountCommand : IRequest<int>
    {
        public SaveBankAccountCommand(BankAccount bankAccount)
        {
            this.BankAccount = bankAccount;
        }

        public BankAccount BankAccount { get; private set; }
    }
}
