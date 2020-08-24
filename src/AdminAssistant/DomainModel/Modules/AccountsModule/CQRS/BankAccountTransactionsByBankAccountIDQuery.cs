using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankAccountTransactionsByBankAccountIDQuery : IRequest<Result<IEnumerable<BankAccountTransaction>>>
    {
        public BankAccountTransactionsByBankAccountIDQuery(int bankAccountID)
        {
            this.BankAccountID = bankAccountID;
        }

        public int BankAccountID { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
        internal class BankAccountTransactionsByBankAccountIDHandler : RequestHandlerBase<BankAccountTransactionsByBankAccountIDQuery, Result<IEnumerable<BankAccountTransaction>>>
        {
            private readonly IBankAccountTransactionRepository bankAccountTransactionRepository;

            public BankAccountTransactionsByBankAccountIDHandler(IBankAccountTransactionRepository bankAccountTransactionRepository, ILoggingProvider loggingProvider)
                : base(loggingProvider)
            {
                this.bankAccountTransactionRepository = bankAccountTransactionRepository;
            }

            public override async Task<Result<IEnumerable<BankAccountTransaction>>> Handle(BankAccountTransactionsByBankAccountIDQuery request, CancellationToken cancellationToken)
            {
                var bankAccountTransactionList = await bankAccountTransactionRepository.GetListAsync(request.BankAccountID).ConfigureAwait(false);

                if (bankAccountTransactionList != null && bankAccountTransactionList.Any())
                    return Result<IEnumerable<BankAccountTransaction>>.Success(bankAccountTransactionList);
                else
                    return Result<IEnumerable<BankAccountTransaction>>.NotFound();
            }
        }
    }
}
