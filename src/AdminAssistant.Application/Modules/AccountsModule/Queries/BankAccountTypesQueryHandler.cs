using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;
using Ardalis.GuardClauses;

namespace AdminAssistant.Modules.AccountsModule.Queries;

public sealed record BankAccountTypesQuery : IRequest<Result<IEnumerable<BankAccountType>>>;

internal sealed class BankAccountTypesQueryHandler(
    IBankAccountTypeRepository bankAccountTypeRepository,
    ILoggingProvider loggingProvider)
    : RequestHandlerBase<BankAccountTypesQuery, Result<IEnumerable<BankAccountType>>>(loggingProvider)
{
    public override async Task<Result<IEnumerable<BankAccountType>>> Handle(BankAccountTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await bankAccountTypeRepository.GetListAsync(cancellationToken).ConfigureAwait(false);
        Guard.Against.Zero(result.Count, message: "BankAccountType list was not populated.");

        return Result<IEnumerable<BankAccountType>>.Success(result);
    }
}
