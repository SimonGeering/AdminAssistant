using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.Modules.AccountsModule.Queries;

internal sealed class BankAccountByIDQueryHandler(ILoggingProvider loggingProvider, IBankAccountRepository bankAccountRepository)
    : RequestHandlerBase<BankAccountByIDQuery, Result<BankAccount>>(loggingProvider)
{
    public override async Task<Result<BankAccount>> Handle(BankAccountByIDQuery request, CancellationToken cancellationToken)
    {
        var bankAccount = await bankAccountRepository.GetAsync(new(request.BankAccountID), cancellationToken).ConfigureAwait(false);

        if (bankAccount == null || bankAccount.BankAccountID.IsUnknownRecordID)
            return Result<BankAccount>.NotFound();

        return Result<BankAccount>.Success(bankAccount);
    }
}
