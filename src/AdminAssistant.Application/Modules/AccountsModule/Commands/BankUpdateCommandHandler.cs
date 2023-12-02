using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;
using AdminAssistant.Modules.AccountsModule.Validation;

namespace AdminAssistant.Modules.AccountsModule.Commands;

public sealed record BankUpdateCommand(Bank Bank) : IRequest<Result<Bank>>;

internal sealed class BankUpdateCommandHandler(
    ILoggingProvider loggingProvider,
    IBankRepository bankRepository,
    IBankValidator bankValidator)
    : RequestHandlerBase<BankUpdateCommand, Result<Bank>>(loggingProvider)
{
    public override async Task<Result<Bank>> Handle(BankUpdateCommand command, CancellationToken cancellationToken)
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
