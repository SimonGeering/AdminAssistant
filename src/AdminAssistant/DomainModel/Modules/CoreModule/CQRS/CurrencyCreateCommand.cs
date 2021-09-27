using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS;

public record CurrencyCreateCommand(Currency Currency) : IRequest<Result<Currency>>;
