using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.CoreModule.CQRS;

public record CurrencyByIDQuery(int CurrencyID) : IRequest<Result<Currency>>;
