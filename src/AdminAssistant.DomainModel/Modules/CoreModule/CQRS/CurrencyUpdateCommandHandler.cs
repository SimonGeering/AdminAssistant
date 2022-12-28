using AdminAssistant.DomainModel.Modules.CoreModule.Validation;
using AdminAssistant.Infra.DAL.Modules.CoreModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS;

internal class CurrencyUpdateCommandHandler : RequestHandlerBase<CurrencyUpdateCommand, Result<Currency>>
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ICurrencyValidator _currencyValidator;

    public CurrencyUpdateCommandHandler(ILoggingProvider loggingProvider, ICurrencyRepository currencyRepository, ICurrencyValidator currencyValidator)
        : base(loggingProvider)
    {
        _currencyRepository = currencyRepository;
        _currencyValidator = currencyValidator;
    }

    public override async Task<Result<Currency>> Handle(CurrencyUpdateCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _currencyValidator.ValidateAsync(command.Currency, cancellationToken).ConfigureAwait(false);

        if (validationResult.IsValid == false)
        {
            return Result<Currency>.Invalid(validationResult.AsErrors());
        }

        var result = await _currencyRepository.SaveAsync(command.Currency).ConfigureAwait(false);
        return Result<Currency>.Success(result);
    }
}
