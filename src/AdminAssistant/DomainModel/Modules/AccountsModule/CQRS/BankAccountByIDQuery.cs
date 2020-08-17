using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankAccountByIDQuery : IRequest<Result<BankAccount>>
    {
        public BankAccountByIDQuery(int bankAccountID)
        {
            this.BankAccountID = bankAccountID;
        }

        public int BankAccountID { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
        internal class BankAccountByIDHandler : RequestHandlerBase<BankAccountByIDQuery, Result<BankAccount>>
        {
            private readonly IBankAccountRepository bankAccountRepository;

            public BankAccountByIDHandler(ILoggingProvider loggingProvider, IBankAccountRepository bankAccountRepository)
                : base(loggingProvider)
            {
                this.bankAccountRepository = bankAccountRepository;
            }

            public override async Task<Result<BankAccount>> Handle(BankAccountByIDQuery request, CancellationToken cancellationToken)
            {
                var bankAccount = await bankAccountRepository.GetAsync(request.BankAccountID).ConfigureAwait(false);

                if (bankAccount == null || bankAccount.BankAccountID == Constants.UnknownRecordID)
                    return Result<BankAccount>.NotFound();

                return Result<BankAccount>.Success(bankAccount);
            }
        }
    }
}
