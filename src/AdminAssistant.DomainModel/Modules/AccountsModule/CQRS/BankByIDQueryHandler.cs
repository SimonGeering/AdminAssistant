using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

internal sealed class BankByIDQueryHandler(IBankRepository bankRepository, ILoggingProvider loggingProvider)
    : RequestHandlerBase<BankByIDQuery, Result<Bank>>(loggingProvider)
{
    public override async Task<Result<Bank>> Handle(BankByIDQuery request, CancellationToken cancellationToken)
    {
        var result = await bankRepository.GetAsync(request.BankID).ConfigureAwait(false);

        if (result == null || result.BankID == Constants.UnknownRecordID)
            return Result<Bank>.NotFound();

        return Result<Bank>.Success(result);
    }
}
