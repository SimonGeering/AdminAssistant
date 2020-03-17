using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
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
