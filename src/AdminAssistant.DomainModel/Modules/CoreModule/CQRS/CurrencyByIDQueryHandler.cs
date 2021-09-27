using AdminAssistant.Infra.DAL.Modules.CoreModule;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS;

internal class CurrencyByIDQueryHandler : RequestHandlerBase<CurrencyByIDQuery, Result<Currency>>
{
    private readonly ICurrencyRepository _currencyRepository;

    public CurrencyByIDQueryHandler(ICurrencyRepository currencyRepository, ILoggingProvider loggingProvider)
        : base(loggingProvider) => _currencyRepository = currencyRepository;

    public override async Task<Result<Currency>> Handle(CurrencyByIDQuery request, CancellationToken cancellationToken)
    {
        var result = await _currencyRepository.GetAsync(request.CurrencyID).ConfigureAwait(false);

        if (result == null || result.CurrencyID == Constants.UnknownRecordID)
            return Result<Currency>.NotFound();

        return Result<Currency>.Success(result);
    }
}
