using AdminAssistant.DomainModel.Modules.CoreModule;

namespace AdminAssistant.Application.Modules.CoreModule.CQRS
{
    public sealed record CurrencyCreateCommand(Currency Currency) : IRequest<Result<Currency>>;
    public sealed record CurrencyUpdateCommand(Currency Currency) : IRequest<Result<Currency>>;
}
