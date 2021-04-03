using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankUpdateCommand : IRequest<Result<Bank>>
    {
        public BankUpdateCommand(Bank bank) => Bank = bank;

        public Bank Bank { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
        internal class BankUpdateHandler : RequestHandlerBase<BankUpdateCommand, Result<Bank>>
        {
            private readonly IBankRepository _bankRepository;
            private readonly IBankValidator _bankValidator;

            public BankUpdateHandler(ILoggingProvider loggingProvider, IBankRepository bankRepository, IBankValidator bankValidator)
                : base(loggingProvider)
            {
                _bankRepository = bankRepository;
                _bankValidator = bankValidator;
            }

            public override async Task<Result<Bank>> Handle(BankUpdateCommand command, CancellationToken cancellationToken)
            {
                var validationResult = await _bankValidator.ValidateAsync(command.Bank, cancellationToken).ConfigureAwait(false);

                if (validationResult.IsValid == false)
                {
                    return Result<Bank>.Invalid(validationResult.AsErrors());
                }

                var result = await _bankRepository.SaveAsync(command.Bank).ConfigureAwait(false);
                return Result<Bank>.Success(result);
            }
        }
    }
}
