using AdminAssistant.Infra.DAL.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.CoreModule.Validation;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS;

internal sealed class CurrencyCreateCommandHandler : RequestHandlerBase<CurrencyCreateCommand, Result<Currency>>
{
    private readonly ICurrencyRepository currencyRepository;
    private readonly ICurrencyValidator currencyValidator;

    public CurrencyCreateCommandHandler(ILoggingProvider loggingProvider, ICurrencyRepository currencyRepository, ICurrencyValidator currencyValidator)
        : base(loggingProvider)
    {
        this.currencyRepository = currencyRepository;
        this.currencyValidator = currencyValidator;
    }

    public override async Task<Result<Currency>> Handle(CurrencyCreateCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await currencyValidator.ValidateAsync(command.Currency, cancellationToken).ConfigureAwait(false);

        if (validationResult.IsValid == false)
        {
            return Result<Currency>.Invalid(validationResult.AsErrors());
        }

        var result = await currencyRepository.SaveAsync(command.Currency).ConfigureAwait(false);
        return Result<Currency>.Success(result);
    }
}
