using AdminAssistant.DomainModel.Modules.CoreModule;

namespace AdminAssistant.Application.Modules.CoreModule.CQRS
{
    public sealed record CurrenciesQuery : IRequest<Result<IEnumerable<Currency>>>;
    public sealed record CurrencyByIDQuery(int CurrencyID) : IRequest<Result<Currency>>;
}
