using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Framework.Providers;
using Ardalis.Result;
using MediatR;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankQuery : IRequest<Result<IEnumerable<Bank>>>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
        internal class BankHandler : RequestHandlerBase<BankQuery, Result<IEnumerable<Bank>>>
        {
            private readonly IBankRepository _bankRepository;

            public BankHandler(IBankRepository bankRepository, ILoggingProvider loggingProvider)
                : base(loggingProvider) => _bankRepository = bankRepository;

            public override async Task<Result<IEnumerable<Bank>>> Handle(BankQuery request, CancellationToken cancellationToken)
            {
                var result = await _bankRepository.GetListAsync().ConfigureAwait(false);

                Trace.Assert(result.Count > 0, "Bank list was not populated.");

                return Result<IEnumerable<Bank>>.Success(result);
            }
        }
    }
}
