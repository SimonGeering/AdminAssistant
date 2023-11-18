using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

internal sealed class BankAccountTransactionsByBankAccountIDQueryHandler(
    IBankAccountTransactionRepository bankAccountTransactionRepository,
    ILoggingProvider loggingProvider)
    : RequestHandlerBase<BankAccountTransactionsByBankAccountIDQuery, Result<IEnumerable<BankAccountTransaction>>>(loggingProvider)
{
    public override async Task<Result<IEnumerable<BankAccountTransaction>>> Handle(BankAccountTransactionsByBankAccountIDQuery request, CancellationToken cancellationToken)
    {
        var bankAccountTransactionList = await bankAccountTransactionRepository.GetListAsync(request.BankAccountID).ConfigureAwait(false);

        if (bankAccountTransactionList == null || bankAccountTransactionList.Count == 0)
            return Result<IEnumerable<BankAccountTransaction>>.NotFound();

        return Result<IEnumerable<BankAccountTransaction>>.Success(bankAccountTransactionList);
    }
}
