using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.Infra.DAL.Modules.CoreModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.Application.Modules.CoreModule.CQRS;

internal sealed class CurrenciesQueryHandler(ICurrencyRepository currencyRepository, ILoggingProvider loggingProvider)
    : RequestHandlerBase<CurrenciesQuery, Result<IEnumerable<Currency>>>(loggingProvider)
{
    public override async Task<Result<IEnumerable<Currency>>> Handle(CurrenciesQuery request, CancellationToken cancellationToken)
    {
        var result = await currencyRepository.GetListAsync(cancellationToken).ConfigureAwait(false);

        Trace.Assert(result.Count > 0, "Currency list was not populated.");

        return Result<IEnumerable<Currency>>.Success(result);
    }
}
