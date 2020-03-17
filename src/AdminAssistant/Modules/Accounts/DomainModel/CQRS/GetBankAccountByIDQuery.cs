using MediatR;

namespace AdminAssistant.Accounts.DomainModel.CQRS
{
    public class GetBankAccountByIDQuery : IRequest<BankAccount>
    {
        public GetBankAccountByIDQuery(int bankAccountID)
        {
            this.BankAccountID = bankAccountID;
        }

        public int BankAccountID { get; private set; }
    }
}
