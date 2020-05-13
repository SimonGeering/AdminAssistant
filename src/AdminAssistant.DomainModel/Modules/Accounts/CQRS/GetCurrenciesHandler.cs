using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.Accounts;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class GetCurrenciesHandler : RequestHandlerBase<GetCurrenciesQuery, Result<IEnumerable<Currency>>>
    {
        private readonly ICurrencyRepository currencyRepository;

        public GetCurrenciesHandler(ICurrencyRepository currencyRepository, ILoggingProvider loggingProvider)
            : base(loggingProvider)
        {
            this.currencyRepository = currencyRepository;
        }

        public override async Task<Result<IEnumerable<Currency>>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
        {
            this.Log.Start();

            var result = await currencyRepository.GetCurrencyListAsync().ConfigureAwait(false);

            Trace.Assert(result.Count > 0, "Currency list was not populated.");

            return this.Log.Finish(Result<IEnumerable<Currency>>.Success(result));
        }
    }
}
