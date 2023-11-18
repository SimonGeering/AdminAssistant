using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

internal sealed class BankQueryHandler(IBankRepository bankRepository, ILoggingProvider loggingProvider)
    : RequestHandlerBase<BankQuery, Result<IEnumerable<Bank>>>(loggingProvider)
{
    public override async Task<Result<IEnumerable<Bank>>> Handle(BankQuery request, CancellationToken cancellationToken)
    {
        var result = await bankRepository.GetListAsync().ConfigureAwait(false);

        Trace.Assert(result.Count > 0, "Bank list was not populated.");

        return Result<IEnumerable<Bank>>.Success(result);
    }
}
