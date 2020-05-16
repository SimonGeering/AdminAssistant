using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.Accounts;
using AdminAssistant.DomainModel.Modules.Accounts.Validation;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class BankAccountCreateHandler : IRequestHandler<BankAccountCreateCommand, Result<BankAccount>>
    {
        private readonly ILoggingProvider log;
        private readonly IBankAccountRepository bankAccountRepository;
        private readonly IBankAccountValidator bankAccountValidator;

        public BankAccountCreateHandler(ILoggingProvider log, IBankAccountRepository bankAccountRepository, IBankAccountValidator bankAccountValidator)
        {
            this.log = log;
            this.bankAccountRepository = bankAccountRepository;
            this.bankAccountValidator = bankAccountValidator;
        }

        public async Task<Result<BankAccount>> Handle(BankAccountCreateCommand command, CancellationToken cancellationToken)
        {
            log.Start();

            var validationResult = await bankAccountValidator.ValidateAsync(command.BankAccount).ConfigureAwait(false);

            if (validationResult.IsValid == false)
            {
                int count = 1;
                var validationErrors = new Dictionary<string, string>();

                foreach (var error in validationResult.Errors.Where(x => x.Severity == FluentValidation.Severity.Error))
                {
                    validationErrors.Add($"{count}_{error.ErrorCode}", error.ErrorMessage);
                    count++;
                }
                return log.Finish(Result<BankAccount>.Invalid(validationErrors));
            }

            return log.Finish(Result<BankAccount>.Success(new BankAccount()));
            //return await bankAccountRepository.Save().ConfigureAwait(false);
        }
    }
}
