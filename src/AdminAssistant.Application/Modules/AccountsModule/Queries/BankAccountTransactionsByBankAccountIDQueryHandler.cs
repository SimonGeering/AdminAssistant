using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

namespace AdminAssistant.Modules.AccountsModule.Queries;

public sealed record BankAccountTransactionsByBankAccountIDQuery(int BankAccountId) : IRequest<Result<IEnumerable<BankAccountTransaction>>>;

internal sealed class BankAccountTransactionsByBankAccountIDQueryHandler(
    IBankAccountTransactionRepository bankAccountTransactionRepository,
    ILoggingProvider loggingProvider)
    : RequestHandlerBase<BankAccountTransactionsByBankAccountIDQuery, Result<IEnumerable<BankAccountTransaction>>>(loggingProvider)
{
    public override async Task<Result<IEnumerable<BankAccountTransaction>>> Handle(BankAccountTransactionsByBankAccountIDQuery request, CancellationToken cancellationToken)
    {
        var bankAccountTransactionList = await bankAccountTransactionRepository.GetListAsync(request.BankAccountId, cancellationToken).ConfigureAwait(false);

        if (bankAccountTransactionList.Count == 0)
            return Result<IEnumerable<BankAccountTransaction>>.NotFound();

        return Result<IEnumerable<BankAccountTransaction>>.Success(bankAccountTransactionList);
    }
}
