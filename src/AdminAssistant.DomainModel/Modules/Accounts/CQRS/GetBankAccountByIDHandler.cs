using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.Accounts;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
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
