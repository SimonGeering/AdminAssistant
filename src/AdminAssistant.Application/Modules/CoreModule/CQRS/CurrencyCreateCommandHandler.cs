using AdminAssistant.Infra.DAL.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.CoreModule.Validation;
using AdminAssistant.Infra.Providers;
using AdminAssistant.DomainModel.Modules.CoreModule;

namespace AdminAssistant.Application.Modules.CoreModule.CQRS;

internal sealed class CurrencyCreateCommandHandler(
    ILoggingProvider loggingProvider,
    ICurrencyRepository currencyRepository,
    ICurrencyValidator currencyValidator)
    : RequestHandlerBase<CurrencyCreateCommand, Result<Currency>>(loggingProvider)
{
    public override async Task<Result<Currency>> Handle(CurrencyCreateCommand command, CancellationToken cancellationToken)
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
