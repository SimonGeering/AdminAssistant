using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

namespace AdminAssistant.Modules.AccountsModule.Queries;

public sealed record BankAccountByIDQuery(int BankAccountId) : IRequest<Result<BankAccount>>;

internal sealed class BankAccountByIDQueryHandler(ILoggingProvider loggingProvider, IBankAccountRepository bankAccountRepository)
    : RequestHandlerBase<BankAccountByIDQuery, Result<BankAccount>>(loggingProvider)
{
    public override async Task<Result<BankAccount>> Handle(BankAccountByIDQuery request, CancellationToken cancellationToken)
    {
        var bankAccount = await bankAccountRepository.GetAsync(new(request.BankAccountId), cancellationToken).ConfigureAwait(false);

        if (bankAccount == null || bankAccount.BankAccountID.IsUnknownRecordId)
            return Result<BankAccount>.NotFound();

        return Result<BankAccount>.Success(bankAccount);
    }
}
