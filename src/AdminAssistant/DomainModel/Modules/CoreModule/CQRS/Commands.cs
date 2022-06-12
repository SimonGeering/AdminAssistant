namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS
{
    public record CurrencyCreateCommand(Currency Currency) : IRequest<Result<Currency>>;
    public record CurrencyUpdateCommand(Currency Currency) : IRequest<Result<Currency>>;
}
