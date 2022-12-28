using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

internal class BankByIDQueryHandler : RequestHandlerBase<BankByIDQuery, Result<Bank>>
{
    private readonly IBankRepository _bankRepository;

    public BankByIDQueryHandler(IBankRepository bankRepository, ILoggingProvider loggingProvider)
        : base(loggingProvider) => _bankRepository = bankRepository;

    public override async Task<Result<Bank>> Handle(BankByIDQuery request, CancellationToken cancellationToken)
    {
        var result = await _bankRepository.GetAsync(request.BankID).ConfigureAwait(false);

        if (result == null || result.BankID == Constants.UnknownRecordID)
            return Result<Bank>.NotFound();

        return Result<Bank>.Success(result);
    }
}
