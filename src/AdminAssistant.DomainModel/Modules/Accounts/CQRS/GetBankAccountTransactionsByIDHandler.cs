using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.Accounts;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;

namespace AdminAssistant.DomainModel.Modules.Accounts.CQRS
{
    public class GetBankAccountTransactionsByIDHandler : RequestHandlerBase<BankAccountTransactionsByBankAccountIDQuery, Result<IEnumerable<BankAccountTransaction>>>
    {
        private readonly IBankAccountRepository bankAccountRepository;

        public GetBankAccountTransactionsByIDHandler(IBankAccountRepository bankAccountRepository, ILoggingProvider loggingProvider)
            : base(loggingProvider)
        {
            this.bankAccountRepository = bankAccountRepository;
        }

        public override async Task<Result<IEnumerable<BankAccountTransaction>>> Handle(BankAccountTransactionsByBankAccountIDQuery request, CancellationToken cancellationToken)
        {
            this.Log.Start();

            var bankAccountTransactionList = await bankAccountRepository.GetBankAccountTransactionListAsync(request.BankAccountID).ConfigureAwait(false);

            if (bankAccountTransactionList != null && bankAccountTransactionList.Any())
                return this.Log.Finish(Result<IEnumerable<BankAccountTransaction>>.Success(bankAccountTransactionList));
            else
                return this.Log.Finish(Result<IEnumerable<BankAccountTransaction>>.NotFound());
        }
    }
}
