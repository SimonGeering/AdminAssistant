using AdminAssistant.Modules.CoreModule.Infrastructure.DAL;
using Ardalis.GuardClauses;

namespace AdminAssistant.Modules.CoreModule.Queries;

public sealed record CurrenciesQuery : IRequest<Result<IEnumerable<Currency>>>;

internal sealed class CurrenciesQueryHandler(ICurrencyRepository currencyRepository, ILoggingProvider loggingProvider)
    : RequestHandlerBase<CurrenciesQuery, Result<IEnumerable<Currency>>>(loggingProvider)
{
    public override async Task<Result<IEnumerable<Currency>>> Handle(CurrenciesQuery request, CancellationToken cancellationToken)
    {
        var result = await currencyRepository.GetListAsync(cancellationToken).ConfigureAwait(false);
        Guard.Against.Zero(result.Count, message: "Currency list was not populated.");

        return Result<IEnumerable<Currency>>.Success(result);
    }
}
