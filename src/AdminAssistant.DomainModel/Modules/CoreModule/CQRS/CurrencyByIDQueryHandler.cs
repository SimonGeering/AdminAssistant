using AdminAssistant.Infra.DAL.Modules.CoreModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS;

internal sealed class CurrencyByIDQueryHandler(ICurrencyRepository currencyRepository, ILoggingProvider loggingProvider)
    : RequestHandlerBase<CurrencyByIDQuery, Result<Currency>>(loggingProvider)
{
    public override async Task<Result<Currency>> Handle(CurrencyByIDQuery request, CancellationToken cancellationToken)
    {
        var result = await currencyRepository.GetAsync(request.CurrencyID).ConfigureAwait(false);

        if (result == null || result.CurrencyID == Constants.UnknownRecordID)
            return Result<Currency>.NotFound();

        return Result<Currency>.Success(result);
    }
}
