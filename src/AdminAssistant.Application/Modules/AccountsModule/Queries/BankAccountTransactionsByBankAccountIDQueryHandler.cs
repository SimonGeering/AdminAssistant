using AdminAssistant.Infra.Providers;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

namespace AdminAssistant.Modules.AccountsModule.Queries;

internal sealed class BankAccountTransactionsByBankAccountIDQueryHandler(
    IBankAccountTransactionRepository bankAccountTransactionRepository,
    ILoggingProvider loggingProvider)
    : RequestHandlerBase<BankAccountTransactionsByBankAccountIDQuery, Result<IEnumerable<BankAccountTransaction>>>(loggingProvider)
{
    public override async Task<Result<IEnumerable<BankAccountTransaction>>> Handle(BankAccountTransactionsByBankAccountIDQuery request, CancellationToken cancellationToken)
    {
        var bankAccountTransactionList = await bankAccountTransactionRepository.GetListAsync(request.BankAccountID, cancellationToken).ConfigureAwait(false);

        if (bankAccountTransactionList == null || bankAccountTransactionList.Count == 0)
            return Result<IEnumerable<BankAccountTransaction>>.NotFound();

        return Result<IEnumerable<BankAccountTransaction>>.Success(bankAccountTransactionList);
    }
}
