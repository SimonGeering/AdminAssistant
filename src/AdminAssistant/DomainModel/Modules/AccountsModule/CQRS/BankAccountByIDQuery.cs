using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankAccountByIDQuery : IRequest<Result<BankAccount>>
    {
        public BankAccountByIDQuery(int bankAccountID) => BankAccountID = bankAccountID;

        public int BankAccountID { get; private set; }

        [SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
        internal class BankAccountByIDHandler : RequestHandlerBase<BankAccountByIDQuery, Result<BankAccount>>
        {
            private readonly IBankAccountRepository _bankAccountRepository;

            public BankAccountByIDHandler(ILoggingProvider loggingProvider, IBankAccountRepository bankAccountRepository)
                : base(loggingProvider) => _bankAccountRepository = bankAccountRepository;

            public override async Task<Result<BankAccount>> Handle(BankAccountByIDQuery request, CancellationToken cancellationToken)
            {
                var bankAccount = await _bankAccountRepository.GetAsync(request.BankAccountID).ConfigureAwait(false);

                if (bankAccount == null || bankAccount.BankAccountID == Constants.UnknownRecordID)
                    return Result<BankAccount>.NotFound();

                return Result<BankAccount>.Success(bankAccount);
            }
        }
    }
}
