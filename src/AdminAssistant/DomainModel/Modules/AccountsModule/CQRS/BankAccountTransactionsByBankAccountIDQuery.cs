using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public record BankAccountTransactionsByBankAccountIDQuery(int BankAccountID) : IRequest<Result<IEnumerable<BankAccountTransaction>>>;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountTransactionsByBankAccountIDHandler : RequestHandlerBase<BankAccountTransactionsByBankAccountIDQuery, Result<IEnumerable<BankAccountTransaction>>>
    {
        private readonly IBankAccountTransactionRepository _bankAccountTransactionRepository;

        public BankAccountTransactionsByBankAccountIDHandler(IBankAccountTransactionRepository bankAccountTransactionRepository, ILoggingProvider loggingProvider)
            : base(loggingProvider) => _bankAccountTransactionRepository = bankAccountTransactionRepository;

        public override async Task<Result<IEnumerable<BankAccountTransaction>>> Handle(BankAccountTransactionsByBankAccountIDQuery request, CancellationToken cancellationToken)
        {
            var bankAccountTransactionList = await _bankAccountTransactionRepository.GetListAsync(request.BankAccountID).ConfigureAwait(false);

            if (bankAccountTransactionList == null || bankAccountTransactionList.Any() == false)
                return Result<IEnumerable<BankAccountTransaction>>.NotFound();

            return Result<IEnumerable<BankAccountTransaction>>.Success(bankAccountTransactionList);
        }
    }
}
