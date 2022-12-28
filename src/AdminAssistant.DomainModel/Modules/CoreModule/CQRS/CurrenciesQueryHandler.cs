using AdminAssistant.Infra.DAL.Modules.CoreModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS;

internal sealed class CurrenciesQueryHandler : RequestHandlerBase<CurrenciesQuery, Result<IEnumerable<Currency>>>
{
    private readonly ICurrencyRepository _currencyRepository;

    public CurrenciesQueryHandler(ICurrencyRepository currencyRepository, ILoggingProvider loggingProvider)
        : base(loggingProvider) => _currencyRepository = currencyRepository;

    public override async Task<Result<IEnumerable<Currency>>> Handle(CurrenciesQuery request, CancellationToken cancellationToken)
    {
        var result = await _currencyRepository.GetListAsync().ConfigureAwait(false);

        Trace.Assert(result.Count > 0, "Currency list was not populated.");

        return Result<IEnumerable<Currency>>.Success(result);
    }
}
