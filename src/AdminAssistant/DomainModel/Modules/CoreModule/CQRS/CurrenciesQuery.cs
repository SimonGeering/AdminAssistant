using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS;

public record CurrenciesQuery : IRequest<Result<IEnumerable<Currency>>>;
