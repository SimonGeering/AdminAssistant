using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

namespace AdminAssistant.Modules.AccountsModule.Queries;

internal sealed class BankQueryHandler(IBankRepository bankRepository, ILoggingProvider loggingProvider)
    : RequestHandlerBase<BankQuery, Result<IEnumerable<Bank>>>(loggingProvider)
{
    public override async Task<Result<IEnumerable<Bank>>> Handle(BankQuery request, CancellationToken cancellationToken)
    {
        var result = await bankRepository.GetListAsync(cancellationToken).ConfigureAwait(false);

        Trace.Assert(result.Count > 0, "Bank list was not populated.");

        return Result<IEnumerable<Bank>>.Success(result);
    }
}
