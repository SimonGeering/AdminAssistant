using AdminAssistant.Modules.CoreModule.Infrastructure.DAL;
using AdminAssistant.Modules.CoreModule.Validation;

namespace AdminAssistant.Modules.CoreModule.Commands;

public sealed record CurrencyCreateCommand(Currency Currency) : IRequest<Result<Currency>>;

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
