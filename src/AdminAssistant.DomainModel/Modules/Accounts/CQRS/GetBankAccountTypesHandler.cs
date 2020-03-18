using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.Accounts;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class GetBankAccountTypesHandler : IRequestHandler<GetBankAccountTypesQuery, IEnumerable<BankAccountType>>
    {
        private readonly IBankAccountTypeRepository bankAccountTypeRepository;

        public GetBankAccountTypesHandler(IBankAccountTypeRepository bankAccountTypeRepository)
        {
            this.bankAccountTypeRepository = bankAccountTypeRepository;
        }

        public async Task<IEnumerable<BankAccountType>> Handle(GetBankAccountTypesQuery request, CancellationToken cancellationToken)
        {
            return await bankAccountTypeRepository.GetBankAccountTypeListAsync().ConfigureAwait(false);
        }
    }
}
