using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

namespace AdminAssistant.Modules.AccountsModule.Queries;

public sealed record BankByIDQuery(int BankId) : IRequest<Result<Bank>>;

internal sealed class BankByIDQueryHandler(IBankRepository bankRepository, ILoggingProvider loggingProvider)
    : RequestHandlerBase<BankByIDQuery, Result<Bank>>(loggingProvider)
{
    public override async Task<Result<Bank>> Handle(BankByIDQuery request, CancellationToken cancellationToken)
    {
        var result = await bankRepository.GetAsync(new BankId(request.BankId), cancellationToken).ConfigureAwait(false);

        if (result == null || result.BankID.IsUnknownRecordId)
            return Result<Bank>.NotFound();

        return Result<Bank>.Success(result);
    }
}
