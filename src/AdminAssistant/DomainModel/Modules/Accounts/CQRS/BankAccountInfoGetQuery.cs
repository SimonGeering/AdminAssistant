using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class BankAccountInfoGetQuery : IRequest<Result<IEnumerable<BankAccountInfo>>>
    {
        public BankAccountInfoGetQuery(int ownerID)
        {
            this.OwnerID = ownerID;
        }
        public int OwnerID { get; }
    }
}
