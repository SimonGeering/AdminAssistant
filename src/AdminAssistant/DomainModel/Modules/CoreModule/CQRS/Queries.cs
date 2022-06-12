namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS
{
    public record CurrenciesQuery : IRequest<Result<IEnumerable<Currency>>>;
    public record CurrencyByIDQuery(int CurrencyID) : IRequest<Result<Currency>>;

}
