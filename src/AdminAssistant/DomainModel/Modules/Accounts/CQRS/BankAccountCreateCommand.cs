using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class BankAccountCreateCommand : IRequest<Result<BankAccount>>
    {
        public BankAccountCreateCommand(BankAccount bankAccount)
        {
            this.BankAccount = bankAccount;
        }

        public BankAccount BankAccount { get; private set; }
    }
}
