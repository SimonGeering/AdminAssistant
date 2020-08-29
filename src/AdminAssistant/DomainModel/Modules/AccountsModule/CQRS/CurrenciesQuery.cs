using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class CurrenciesQuery : IRequest<Result<IEnumerable<Currency>>>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
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
    }
}
