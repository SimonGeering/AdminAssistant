using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class GetBankAccountInfoQuery : IRequest<Result<IEnumerable<BankAccountInfo>>>
    {
        public GetBankAccountInfoQuery(int ownerID)
        {
            this.OwnerID = ownerID;
        }
        public int OwnerID { get; }
    }
}
