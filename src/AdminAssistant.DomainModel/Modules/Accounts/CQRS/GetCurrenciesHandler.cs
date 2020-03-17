using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Accounts.DAL;
using MediatR;

namespace AdminAssistant.Accounts.DomainModel.CQRS
{
    public class GetCurrenciesHandler : IRequestHandler<GetCurrenciesQuery, IEnumerable<Currency>>
    {
        private readonly ICurrencyRepository currencyRepository;

        public GetCurrenciesHandler(ICurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }

        public async Task<IEnumerable<Currency>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
        {
            return await currencyRepository.GetCurrencyListAsync().ConfigureAwait(false);
        }
    }
}
