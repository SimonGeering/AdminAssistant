using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

internal class BankAccountTransactionsByBankAccountIDQueryHandler : RequestHandlerBase<BankAccountTransactionsByBankAccountIDQuery, Result<IEnumerable<BankAccountTransaction>>>
{
    private readonly IBankAccountTransactionRepository _bankAccountTransactionRepository;

    public BankAccountTransactionsByBankAccountIDQueryHandler(IBankAccountTransactionRepository bankAccountTransactionRepository, ILoggingProvider loggingProvider)
        : base(loggingProvider) => _bankAccountTransactionRepository = bankAccountTransactionRepository;

    public override async Task<Result<IEnumerable<BankAccountTransaction>>> Handle(BankAccountTransactionsByBankAccountIDQuery request, CancellationToken cancellationToken)
    {
        var bankAccountTransactionList = await _bankAccountTransactionRepository.GetListAsync(request.BankAccountID).ConfigureAwait(false);

        if (bankAccountTransactionList == null || bankAccountTransactionList.Any() == false)
            return Result<IEnumerable<BankAccountTransaction>>.NotFound();

        return Result<IEnumerable<BankAccountTransaction>>.Success(bankAccountTransactionList);
    }
}
