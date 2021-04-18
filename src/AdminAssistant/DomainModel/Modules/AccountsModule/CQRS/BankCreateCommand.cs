using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using MediatR;
using Ardalis.Result.FluentValidation;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public record BankCreateCommand(Bank Bank) : IRequest<Result<Bank>>;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankCreateHandler : RequestHandlerBase<BankCreateCommand, Result<Bank>>
    {
        private readonly IBankRepository bankRepository;
        private readonly IBankValidator bankValidator;

        public BankCreateHandler(ILoggingProvider loggingProvider, IBankRepository bankRepository, IBankValidator bankValidator)
            : base(loggingProvider)
        {
            this.bankRepository = bankRepository;
            this.bankValidator = bankValidator;
        }

        public override async Task<Result<Bank>> Handle(BankCreateCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await bankValidator.ValidateAsync(command.Bank, cancellationToken).ConfigureAwait(false);

            if (validationResult.IsValid == false)
            {
                return Result<Bank>.Invalid(validationResult.AsErrors());
            }

            var result = await bankRepository.SaveAsync(command.Bank).ConfigureAwait(false);
            return Result<Bank>.Success(result);
        }
    }
}
