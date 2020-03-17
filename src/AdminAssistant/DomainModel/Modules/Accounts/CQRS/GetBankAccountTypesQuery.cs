using System.Collections.Generic;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class GetBankAccountTypesQuery : IRequest<IEnumerable<BankAccountType>>
    {
    }
}
