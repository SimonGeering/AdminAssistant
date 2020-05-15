using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.Accounts;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class GetBankAccountInfoHandler : RequestHandlerBase<BankAccountInfoGetQuery, Result<IEnumerable<BankAccountInfo>>>
    {
        private readonly IBankAccountRepository bankAccountRepository;

        public GetBankAccountInfoHandler(IBankAccountRepository bankAccountRepository, ILoggingProvider loggingProvider)
            : base(loggingProvider)
        {
            this.bankAccountRepository = bankAccountRepository;
        }

        public override async Task<Result<IEnumerable<BankAccountInfo>>> Handle(BankAccountInfoGetQuery request, CancellationToken cancellationToken)
        {
            this.Log.Start();

            var bankAccountInfoList = await bankAccountRepository.GetBankAccountInfoListAsync(request.OwnerID).ConfigureAwait(false);
            return this.Log.Finish(Result<IEnumerable<BankAccountInfo>>.Success(bankAccountInfoList));
        }
    }
}
