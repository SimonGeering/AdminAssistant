using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class GetBankAccountTypesQuery : IRequest<Result<IEnumerable<BankAccountType>>>
    {
    }
}
