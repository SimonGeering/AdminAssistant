using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;
using AdminAssistant.Modules.AccountsModule.Validation;

namespace AdminAssistant.Modules.AccountsModule.Commands;

public sealed record BankCreateCommand(Bank Bank) : IRequest<Result<Bank>>;

internal sealed class BankCreateCommandHandler(
    ILoggingProvider loggingProvider,
    IBankRepository bankRepository,
    IBankValidator bankValidator)
    : RequestHandlerBase<BankCreateCommand, Result<Bank>>(loggingProvider)
{
    public override async Task<Result<Bank>> Handle(BankCreateCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await bankValidator.ValidateAsync(command.Bank, cancellationToken).ConfigureAwait(false);

        if (validationResult.IsValid == false)
        {
            return Result<Bank>.Invalid(validationResult.AsErrors());
        }

        var result = await bankRepository.SaveAsync(command.Bank, cancellationToken).ConfigureAwait(false);
        return Result<Bank>.Success(result);
    }
}
