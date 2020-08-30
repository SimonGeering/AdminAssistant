using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.BudgetModule.CQRS
{
    public class BudgetQuery : IRequest<Result<IEnumerable<Budget>>>
    {
    }
}
