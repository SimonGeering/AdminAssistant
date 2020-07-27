using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankAccountTransactionsByBankAccountIDQuery : IRequest<Result<IEnumerable<BankAccountTransaction>>>
    {
        public BankAccountTransactionsByBankAccountIDQuery(int bankAccountID)
        {
            this.BankAccountID = bankAccountID;
        }

        public int BankAccountID { get; private set; }
    }
}
