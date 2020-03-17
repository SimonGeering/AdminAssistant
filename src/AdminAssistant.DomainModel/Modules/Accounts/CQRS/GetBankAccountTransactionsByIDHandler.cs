using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.Accounts;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class GetBankAccountTransactionsByIDHandler : IRequestHandler<GetBankAccountTransactionsByIDQuery, IEnumerable<BankAccountTransaction>>
    {
        private readonly IBankAccountRepository bankAccountRepository;

        public GetBankAccountTransactionsByIDHandler(IBankAccountRepository bankAccountRepository)
        {
            this.bankAccountRepository = bankAccountRepository;
        }

        public async Task<IEnumerable<BankAccountTransaction>> Handle(GetBankAccountTransactionsByIDQuery request, CancellationToken cancellationToken)
        {
            return await bankAccountRepository.GetBankAccountTransactionListAsync(request.BankAccountID).ConfigureAwait(false);
        }
    }
}
