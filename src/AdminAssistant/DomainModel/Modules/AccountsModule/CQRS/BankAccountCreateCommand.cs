using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankAccountCreateCommand : IRequest<Result<BankAccount>>
    {
        public BankAccountCreateCommand(BankAccount bankAccount)
        {
            this.BankAccount = bankAccount;
        }

        public BankAccount BankAccount { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
        internal class BankAccountCreateHandler : IRequestHandler<BankAccountCreateCommand, Result<BankAccount>>
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

                // Don't need this for now as the calidator no longer needs extra context.
                // Keep it here for reference of how to do this.
                //
                // var ctx = new FluentValidation.ValidationContext<BankAccount>(command.BankAccount);
                //ctx.RootContextData[Constants.IsCreateCommandContext] = true;

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

                var result = await bankAccountRepository.SaveAsync(command.BankAccount).ConfigureAwait(false);
                return log.Finish(Result<BankAccount>.Success(result));
            }
        }
    }
}
