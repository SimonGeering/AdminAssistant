using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Accounts.DAL;
using MediatR;

namespace AdminAssistant.Accounts.DomainModel.CQRS
{
    public class GetBankAccountByIDHandler : IRequestHandler<GetBankAccountByIDQuery, BankAccount>
    {
        private readonly IBankAccountRepository bankAccountRepository;

        public GetBankAccountByIDHandler(IBankAccountRepository bankAccountRepository)
        {
            this.bankAccountRepository = bankAccountRepository;
        }

        public async Task<BankAccount> Handle(GetBankAccountByIDQuery request, CancellationToken cancellationToken)
        {
            return await bankAccountRepository.GetBankAccountAsync(request.BankAccountID).ConfigureAwait(false);
        }
    }
}
