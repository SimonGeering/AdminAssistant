using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.CoreModule;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS
{
    public record CurrencyByIDQuery(int CurrencyID) : IRequest<Result<Currency>>;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class CurrencyByIDHandler : RequestHandlerBase<CurrencyByIDQuery, Result<Currency>>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyByIDHandler(ICurrencyRepository currencyRepository, ILoggingProvider loggingProvider)
            : base(loggingProvider) => _currencyRepository = currencyRepository;

        public override async Task<Result<Currency>> Handle(CurrencyByIDQuery request, CancellationToken cancellationToken)
        {
            var result = await _currencyRepository.GetAsync(request.CurrencyID).ConfigureAwait(false);

            if (result == null || result.CurrencyID == Constants.UnknownRecordID)
                return Result<Currency>.NotFound();

            return Result<Currency>.Success(result);
        }
    }
}
