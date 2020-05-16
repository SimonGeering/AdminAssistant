using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.Accounts;
using AdminAssistant.DomainModel.Modules.Accounts.Validation;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class BankAccountUpdateHandler : IRequestHandler<BankAccountUpdateCommand, Result<BankAccount>>
    {
        private readonly IBankAccountRepository bankAccountRepository;
        private readonly IBankAccountValidator bankAccountValidator;

        public BankAccountUpdateHandler(IBankAccountRepository bankAccountRepository, IBankAccountValidator bankAccountValidator)
        {
            this.bankAccountRepository = bankAccountRepository;
            this.bankAccountValidator = bankAccountValidator;
        }

        public async Task<Result<BankAccount>> Handle(BankAccountUpdateCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await bankAccountValidator.ValidateAsync(command.BankAccount).ConfigureAwait(false);
            throw new System.NotImplementedException();
            //var result = this.bankAccountValidator.Validate(command.BankAccount);

            //if (result.IsValid == false)


            //return await bankAccountRepository.Save().ConfigureAwait(false);
        }
    }
}
