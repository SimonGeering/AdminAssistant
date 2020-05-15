using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.Accounts;
using AdminAssistant.DomainModel.Modules.Accounts.Validation;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class BankAccountCreateHandler : IRequestHandler<BankAccountCreateCommand, Result<BankAccount>>
    {
        private readonly IBankAccountRepository bankAccountRepository;
        private readonly IBankAccountValidator bankAccountValidator;

        public BankAccountCreateHandler(IBankAccountRepository bankAccountRepository, IBankAccountValidator bankAccountValidator)
        {
            this.bankAccountRepository = bankAccountRepository;
            this.bankAccountValidator = bankAccountValidator;
        }

        public async Task<Result<BankAccount>> Handle(BankAccountCreateCommand command, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
            //var result = this.bankAccountValidator.Validate(command.BankAccount);

            //if (result.IsValid == false)


            //return await bankAccountRepository.Save().ConfigureAwait(false);
        }
    }
}
