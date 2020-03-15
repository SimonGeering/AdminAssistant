using System.Collections.Generic;
using MediatR;

namespace AdminAssistant.Accounts.DomainModel.CQRS
{
    public class GetCurrenciesQuery : IRequest<IEnumerable<Currency>>
    {
    }
}
