using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankAccountUpdateHandler : IRequestHandler<BankAccountUpdateCommand, Result<BankAccount>>
    {
        private readonly ILoggingProvider log;
        private readonly IBankAccountRepository bankAccountRepository;
        private readonly IBankAccountValidator bankAccountValidator;

        public BankAccountUpdateHandler(ILoggingProvider log, IBankAccountRepository bankAccountRepository, IBankAccountValidator bankAccountValidator)
        {
            this.log = log;
            this.bankAccountRepository = bankAccountRepository;
            this.bankAccountValidator = bankAccountValidator;
        }

        public async Task<Result<BankAccount>> Handle(BankAccountUpdateCommand command, CancellationToken cancellationToken)
        {
            log.Start();

            var validationResult = await bankAccountValidator.ValidateAsync(command.BankAccount).ConfigureAwait(false);
            throw new System.NotImplementedException();
            //var result = this.bankAccountValidator.Validate(command.BankAccount);

            //if (result.IsValid == false)


            //return await bankAccountRepository.Save().ConfigureAwait(false);
        }
    }
}