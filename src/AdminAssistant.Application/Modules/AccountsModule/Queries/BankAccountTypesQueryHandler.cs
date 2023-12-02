using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.Modules.AccountsModule.Queries;

internal sealed class BankAccountTypesQueryHandler(
    IBankAccountTypeRepository bankAccountTypeRepository,
    ILoggingProvider loggingProvider)
    : RequestHandlerBase<BankAccountTypesQuery, Result<IEnumerable<BankAccountType>>>(loggingProvider)
{
    public override async Task<Result<IEnumerable<BankAccountType>>> Handle(BankAccountTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await bankAccountTypeRepository.GetListAsync(cancellationToken).ConfigureAwait(false);

        Trace.Assert(result.Count > 0, "BankAccountType list was not populated.");

        return Result<IEnumerable<BankAccountType>>.Success(result);
    }
}
