using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankAccountTypesQuery : IRequest<Result<IEnumerable<BankAccountType>>>
    {
    }
}
