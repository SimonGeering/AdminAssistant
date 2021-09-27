using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS;

public record CurrencyUpdateCommand(Currency Currency) : IRequest<Result<Currency>>;
