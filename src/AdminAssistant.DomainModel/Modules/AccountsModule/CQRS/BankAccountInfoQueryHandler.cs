using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

internal class BankAccountInfoQueryHandler : RequestHandlerBase<BankAccountInfoQuery, Result<IEnumerable<BankAccountInfo>>>
{
    private readonly IBankAccountInfoRepository _bankAccountInfoRepository;

    public BankAccountInfoQueryHandler(IBankAccountInfoRepository bankAccountInfoRepository, ILoggingProvider loggingProvider)
        : base(loggingProvider) => _bankAccountInfoRepository = bankAccountInfoRepository;

    public override async Task<Result<IEnumerable<BankAccountInfo>>> Handle(BankAccountInfoQuery request, CancellationToken cancellationToken)
    {
        // TODO: implement owned entities - pass in request.OwnerID
        var bankAccountInfoList = await _bankAccountInfoRepository.GetListAsync().ConfigureAwait(false);
        return Result<IEnumerable<BankAccountInfo>>.Success(bankAccountInfoList);
    }
}
