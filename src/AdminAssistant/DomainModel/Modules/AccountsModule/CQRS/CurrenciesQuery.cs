using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class CurrenciesQuery : IRequest<Result<IEnumerable<Currency>>>
    {
    }
}
