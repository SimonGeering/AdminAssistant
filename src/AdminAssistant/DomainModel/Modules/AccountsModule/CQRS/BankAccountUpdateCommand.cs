using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankAccountUpdateCommand : IRequest<Result<BankAccount>>
    {
        public BankAccountUpdateCommand(BankAccount bankAccount)
        {
            this.BankAccount = bankAccount;
        }

        public BankAccount BankAccount { get; private set; }
    }
}
