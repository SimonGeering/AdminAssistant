using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

internal sealed class BankAccountInfoQueryHandler(
    IBankAccountInfoRepository bankAccountInfoRepository,
    ILoggingProvider loggingProvider)
    : RequestHandlerBase<BankAccountInfoQuery, Result<IEnumerable<BankAccountInfo>>>(loggingProvider)
{
    public override async Task<Result<IEnumerable<BankAccountInfo>>> Handle(BankAccountInfoQuery request, CancellationToken cancellationToken)
    {
        // TODO: implement owned entities - pass in request.OwnerID
        var bankAccountInfoList = await bankAccountInfoRepository.GetListAsync().ConfigureAwait(false);
        return Result<IEnumerable<BankAccountInfo>>.Success(bankAccountInfoList);
    }
}
