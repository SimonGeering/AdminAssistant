using System.Diagnostics;
using AdminAssistant.Infra.DAL.Modules.CoreModule;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS;

public record CurrenciesQuery : IRequest<Result<IEnumerable<Currency>>>;

internal class CurrenciesHandler : RequestHandlerBase<CurrenciesQuery, Result<IEnumerable<Currency>>>
{
    private readonly ICurrencyRepository _currencyRepository;

    public CurrenciesHandler(ICurrencyRepository currencyRepository, ILoggingProvider loggingProvider)
        : base(loggingProvider) => _currencyRepository = currencyRepository;

    public override async Task<Result<IEnumerable<Currency>>> Handle(CurrenciesQuery request, CancellationToken cancellationToken)
    {
        var result = await _currencyRepository.GetListAsync().ConfigureAwait(false);

        Trace.Assert(result.Count > 0, "Currency list was not populated.");

        return Result<IEnumerable<Currency>>.Success(result);
    }
}
