using System.Collections.Generic;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class GetBankAccountInfoQuery : IRequest<IEnumerable<BankAccountInfo>>
    {
    }
}
