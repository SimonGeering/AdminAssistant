using System.Collections.Generic;
using MediatR;

namespace AdminAssistant.Accounts.DomainModel.CQRS
{
    public class GetBankAccountTypesQuery : IRequest<IEnumerable<BankAccountType>>
    {
    }
}
