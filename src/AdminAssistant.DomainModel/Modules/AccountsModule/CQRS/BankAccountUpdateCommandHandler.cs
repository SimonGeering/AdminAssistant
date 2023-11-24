using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

internal sealed class BankAccountUpdateCommandHandler(
    ILoggingProvider loggingProvider,
    IBankAccountRepository bankAccountRepository,
    IBankAccountValidator bankAccountValidator)
    : RequestHandlerBase<BankAccountUpdateCommand, Result<BankAccount>>(loggingProvider)
{
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
