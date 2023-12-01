using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.Infra.DAL.Modules.CoreModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.Application.Modules.CoreModule.CQRS;

internal sealed class CurrencyByIDQueryHandler(ICurrencyRepository currencyRepository, ILoggingProvider loggingProvider)
    : RequestHandlerBase<CurrencyByIDQuery, Result<Currency>>(loggingProvider)
{
    public override async Task<Result<Currency>> Handle(CurrencyByIDQuery request, CancellationToken cancellationToken)
    {
        var result = await currencyRepository.GetAsync(new CurrencyId(request.CurrencyID), cancellationToken).ConfigureAwait(false);

        if (result == null || result.CurrencyID.IsUnknownRecordID)
            return Result<Currency>.NotFound();

        return Result<Currency>.Success(result);
    }
}
