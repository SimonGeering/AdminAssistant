using AdminAssistant.Infra.Providers;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

namespace AdminAssistant.Modules.AccountsModule.Queries;

internal sealed class BankByIDQueryHandler(IBankRepository bankRepository, ILoggingProvider loggingProvider)
    : RequestHandlerBase<BankByIDQuery, Result<Bank>>(loggingProvider)
{
    public override async Task<Result<Bank>> Handle(BankByIDQuery request, CancellationToken cancellationToken)
    {
        var result = await bankRepository.GetAsync(new BankId(request.BankID), cancellationToken).ConfigureAwait(false);

        if (result == null || result.BankID.IsUnknownRecordID)
            return Result<Bank>.NotFound();

        return Result<Bank>.Success(result);
    }
}
