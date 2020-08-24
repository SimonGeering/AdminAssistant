using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
        internal class BankAccountUpdateHandler : RequestHandlerBase<BankAccountUpdateCommand, Result<BankAccount>>
        {
            private readonly IBankAccountRepository bankAccountRepository;
            private readonly IBankAccountValidator bankAccountValidator;

            public BankAccountUpdateHandler(ILoggingProvider loggingProvider, IBankAccountRepository bankAccountRepository, IBankAccountValidator bankAccountValidator)
                : base(loggingProvider)
            {
                this.bankAccountRepository = bankAccountRepository;
                this.bankAccountValidator = bankAccountValidator;
            }

            public override async Task<Result<BankAccount>> Handle(BankAccountUpdateCommand command, CancellationToken cancellationToken)
            {
                var validationResult = await bankAccountValidator.ValidateAsync(command.BankAccount, cancellationToken).ConfigureAwait(false);

                if (validationResult.IsValid == false)
                {
                    return Result<BankAccount>.Invalid(validationResult.AsErrors());
                }

                var result = await bankAccountRepository.SaveAsync(command.BankAccount).ConfigureAwait(false);
                return Result<BankAccount>.Success(result);
            }
        }
    }
}
