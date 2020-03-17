using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Accounts.DAL;
using MediatR;

namespace AdminAssistant.Accounts.DomainModel.CQRS
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
