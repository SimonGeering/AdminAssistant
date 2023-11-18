using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

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

        var result = await bankRepository.SaveAsync(command.Bank).ConfigureAwait(false);
        return Result<Bank>.Success(result);
    }
}
