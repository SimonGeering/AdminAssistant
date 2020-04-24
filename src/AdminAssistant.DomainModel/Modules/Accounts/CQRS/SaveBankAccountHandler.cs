using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.Accounts;
using AdminAssistant.DomainModel.Modules.Accounts.Validation;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class SaveBankAccountHandler : IRequestHandler<SaveBankAccountCommand, int>
    {
        private readonly IBankAccountRepository bankAccountRepository;
        private readonly IBankAccountValidator bankAccountValidator;

        public SaveBankAccountHandler(IBankAccountRepository bankAccountRepository, IBankAccountValidator bankAccountValidator)
        {
            this.bankAccountRepository = bankAccountRepository;
            this.bankAccountValidator = bankAccountValidator;
        }

        public async Task<int> Handle(SaveBankAccountCommand command, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
            //var result = this.bankAccountValidator.Validate(command.BankAccount);

            //if (result.IsValid == false)


            //return await bankAccountRepository.Save().ConfigureAwait(false);
        }
    }
}
