using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.Accounts;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class GetBankAccountInfoHandler : IRequestHandler<GetBankAccountInfoQuery, IEnumerable<BankAccountInfo>>
    {
        private readonly IBankAccountRepository bankAccountRepository;

        public GetBankAccountInfoHandler(IBankAccountRepository bankAccountRepository)
        {
            this.bankAccountRepository = bankAccountRepository;
        }

        public async Task<IEnumerable<BankAccountInfo>> Handle(GetBankAccountInfoQuery request, CancellationToken cancellationToken)
        {
            // TODO: Hard coded user ID.
            return await bankAccountRepository.GetBankAccountInfoListAsync(10).ConfigureAwait(false);
        }
    }
}
