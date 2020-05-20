using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.Accounts;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class BankAccountInfoHandler : RequestHandlerBase<BankAccountInfoQuery, Result<IEnumerable<BankAccountInfo>>>
    {
        private readonly IBankAccountRepository bankAccountRepository;

        public BankAccountInfoHandler(IBankAccountRepository bankAccountRepository, ILoggingProvider loggingProvider)
            : base(loggingProvider)
        {
            this.bankAccountRepository = bankAccountRepository;
        }

        public override async Task<Result<IEnumerable<BankAccountInfo>>> Handle(BankAccountInfoQuery request, CancellationToken cancellationToken)
        {
            this.Log.Start();

            var bankAccountInfoList = await bankAccountRepository.GetBankAccountInfoListAsync(request.OwnerID).ConfigureAwait(false);
            return this.Log.Finish(Result<IEnumerable<BankAccountInfo>>.Success(bankAccountInfoList));
        }
    }
}
