using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.Accounts;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class BankAccountTypesHandler : RequestHandlerBase<BankAccountTypesQuery, Result<IEnumerable<BankAccountType>>>
    {
        private readonly IBankAccountTypeRepository bankAccountTypeRepository;

        public BankAccountTypesHandler(IBankAccountTypeRepository bankAccountTypeRepository, ILoggingProvider loggingProvider)
            : base(loggingProvider)
        {
            this.bankAccountTypeRepository = bankAccountTypeRepository;
        }

        public override async Task<Result<IEnumerable<BankAccountType>>> Handle(BankAccountTypesQuery request, CancellationToken cancellationToken)
        {
            this.Log.Start();

            var result = await bankAccountTypeRepository.GetBankAccountTypeListAsync().ConfigureAwait(false);

            Trace.Assert(result.Count > 0, "BankAccountType list was not populated.");

            return this.Log.Finish(Result<IEnumerable<BankAccountType>>.Success(result));
        }
    }
}
