using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankAccountInfoQuery : IRequest<Result<IEnumerable<BankAccountInfo>>>
    {
        public BankAccountInfoQuery(int ownerID) => OwnerID = ownerID;

        public int OwnerID { get; }

        [SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
        internal class BankAccountInfoHandler : RequestHandlerBase<BankAccountInfoQuery, Result<IEnumerable<BankAccountInfo>>>
        {
            private readonly IBankAccountInfoRepository _bankAccountInfoRepository;

            public BankAccountInfoHandler(IBankAccountInfoRepository bankAccountInfoRepository, ILoggingProvider loggingProvider)
                : base(loggingProvider) => _bankAccountInfoRepository = bankAccountInfoRepository;

            public override async Task<Result<IEnumerable<BankAccountInfo>>> Handle(BankAccountInfoQuery request, CancellationToken cancellationToken)
            {
                // TODO: implement owned entities - pass in request.OwnerID
                var bankAccountInfoList = await _bankAccountInfoRepository.GetListAsync().ConfigureAwait(false);
                return Result<IEnumerable<BankAccountInfo>>.Success(bankAccountInfoList);
            }
        }
    }
}
