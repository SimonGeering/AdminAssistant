using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

namespace AdminAssistant.Modules.AccountsModule.Queries;

public sealed record BankAccountInfoQuery(int OwnerId) : IRequest<Result<IEnumerable<BankAccountInfo>>>;

internal sealed class BankAccountInfoQueryHandler(
    IBankAccountInfoRepository bankAccountInfoRepository,
    ILoggingProvider loggingProvider)
    : RequestHandlerBase<BankAccountInfoQuery, Result<IEnumerable<BankAccountInfo>>>(loggingProvider)
{
    public override async Task<Result<IEnumerable<BankAccountInfo>>> Handle(BankAccountInfoQuery request, CancellationToken cancellationToken)
    {
        // TODO: implement owned entities - pass in request.OwnerID
        var bankAccountInfoList = await bankAccountInfoRepository.GetListAsync(cancellationToken).ConfigureAwait(false);
        return Result<IEnumerable<BankAccountInfo>>.Success(bankAccountInfoList);
    }
}
