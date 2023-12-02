using AdminAssistant.Modules.AccountsModule.Validation;
using AdminAssistant.Infra.Providers;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

namespace AdminAssistant.Modules.AccountsModule.Commands;

public sealed record BankAccountUpdateCommand(BankAccount BankAccount) : IRequest<Result<BankAccount>>;

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

        var result = await bankAccountRepository.SaveAsync(command.BankAccount, cancellationToken).ConfigureAwait(false);
        return Result<BankAccount>.Success(result);
    }
}
