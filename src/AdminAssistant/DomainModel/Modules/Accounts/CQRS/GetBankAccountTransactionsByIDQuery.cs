using System.Collections.Generic;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class GetBankAccountTransactionsByIDQuery : IRequest<IEnumerable<BankAccountTransaction>>
    {
        public GetBankAccountTransactionsByIDQuery(int bankAccountID)
        {
            this.BankAccountID = bankAccountID;
        }

        public int BankAccountID { get; private set; }
    }
}
