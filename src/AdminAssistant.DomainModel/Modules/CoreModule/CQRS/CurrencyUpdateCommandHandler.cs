using AdminAssistant.DomainModel.Modules.CoreModule.Validation;
using AdminAssistant.Infra.DAL.Modules.CoreModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS;

internal sealed class CurrencyUpdateCommandHandler(
    ILoggingProvider loggingProvider,
    ICurrencyRepository currencyRepository,
    ICurrencyValidator currencyValidator)
    : RequestHandlerBase<CurrencyUpdateCommand, Result<Currency>>(loggingProvider)
{
    public override async Task<Result<Currency>> Handle(CurrencyUpdateCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await currencyValidator.ValidateAsync(command.Currency, cancellationToken).ConfigureAwait(false);

        if (validationResult.IsValid == false)
        {
            return Result<Currency>.Invalid(validationResult.AsErrors());
        }

        var result = await currencyRepository.SaveAsync(command.Currency, cancellationToken).ConfigureAwait(false);
        return Result<Currency>.Success(result);
    }
}
