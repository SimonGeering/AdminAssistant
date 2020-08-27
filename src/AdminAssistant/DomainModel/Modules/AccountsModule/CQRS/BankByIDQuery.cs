using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankByIDQuery : IRequest<Result<Bank>>
    {
        public BankByIDQuery(int bankID) => BankID = bankID;

        public int BankID { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
        internal class BankByIDHandler : RequestHandlerBase<BankByIDQuery, Result<Bank>>
        {
            private readonly IBankRepository _bankRepository;

            public BankByIDHandler(IBankRepository bankRepository, ILoggingProvider loggingProvider)
                : base(loggingProvider) => _bankRepository = bankRepository;

            public override async Task<Result<Bank>> Handle(BankByIDQuery request, CancellationToken cancellationToken)
            {
                var result = await _bankRepository.GetAsync(request.BankID).ConfigureAwait(false);

                if (result == null || result.BankID == Constants.UnknownRecordID)
                    return Result<Bank>.NotFound();

                return Result<Bank>.Success(result);
            }
        }
    }
}
