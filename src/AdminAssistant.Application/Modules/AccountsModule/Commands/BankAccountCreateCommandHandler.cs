using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;
using AdminAssistant.Modules.AccountsModule.Validation;

namespace AdminAssistant.Modules.AccountsModule.Commands;

public sealed record BankAccountCreateCommand(BankAccount BankAccount) : IRequest<Result<BankAccount>>;

internal sealed class BankAccountCreateCommandHandler(
    ILoggingProvider loggingProvider,
    IBankAccountRepository bankAccountRepository,
    IBankAccountValidator bankAccountValidator)
    : RequestHandlerBase<BankAccountCreateCommand, Result<BankAccount>>(loggingProvider)
{
    public override async ValueTask<Result<BankAccount>> Handle(BankAccountCreateCommand command, CancellationToken cancellationToken)
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
