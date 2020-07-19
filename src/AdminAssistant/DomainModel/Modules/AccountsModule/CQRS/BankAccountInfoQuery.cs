using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankAccountInfoQuery : IRequest<Result<IEnumerable<BankAccountInfo>>>
    {
        public BankAccountInfoQuery(int ownerID)
        {
            this.OwnerID = ownerID;
        }
        public int OwnerID { get; }
    }
}
