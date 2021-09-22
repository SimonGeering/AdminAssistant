using AdminAssistant.Infra.DAL.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.CoreModule.Validation;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using MediatR;
using Ardalis.Result.FluentValidation;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS;

public record CurrencyCreateCommand(Currency Currency) : IRequest<Result<Currency>>;

internal class CurrencyCreateHandler : RequestHandlerBase<CurrencyCreateCommand, Result<Currency>>
{
    private readonly ICurrencyRepository currencyRepository;
    private readonly ICurrencyValidator currencyValidator;

    public CurrencyCreateHandler(ILoggingProvider loggingProvider, ICurrencyRepository currencyRepository, ICurrencyValidator currencyValidator)
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
