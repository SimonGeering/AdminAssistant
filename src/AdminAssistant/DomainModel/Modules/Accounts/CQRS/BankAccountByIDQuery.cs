using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class BankAccountByIDQuery : IRequest<Result<BankAccount>>
    {
        public BankAccountByIDQuery(int bankAccountID)
        {
            this.BankAccountID = bankAccountID;
        }

        public int BankAccountID { get; private set; }
    }
}
