using AdminAssistant.Modules.CoreModule.Infrastructure.DAL;

namespace AdminAssistant.Modules.CoreModule.Queries;

public sealed record CurrencyByIDQuery(int CurrencyID) : IRequest<Result<Currency>>;

internal sealed class CurrencyByIDQueryHandler(ICurrencyRepository currencyRepository, ILoggingProvider loggingProvider)
    : RequestHandlerBase<CurrencyByIDQuery, Result<Currency>>(loggingProvider)
{
    public override async ValueTask<Result<Currency>> Handle(CurrencyByIDQuery request, CancellationToken cancellationToken)
    {
        var result = await currencyRepository.GetAsync(new CurrencyId(request.CurrencyID), cancellationToken).ConfigureAwait(false);

        if (result == null || result.CurrencyID.IsUnknownRecordId)
            return Result<Currency>.NotFound();

        return Result<Currency>.Success(result);
    }
}
