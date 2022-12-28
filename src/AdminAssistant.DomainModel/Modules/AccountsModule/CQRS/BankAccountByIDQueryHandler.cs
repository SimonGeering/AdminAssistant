using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

internal sealed class BankAccountByIDQueryHandler : RequestHandlerBase<BankAccountByIDQuery, Result<BankAccount>>
{
    private readonly IBankAccountRepository _bankAccountRepository;

    public BankAccountByIDQueryHandler(ILoggingProvider loggingProvider, IBankAccountRepository bankAccountRepository)
        : base(loggingProvider) => _bankAccountRepository = bankAccountRepository;

    public override async Task<Result<BankAccount>> Handle(BankAccountByIDQuery request, CancellationToken cancellationToken)
    {
        var bankAccount = await _bankAccountRepository.GetAsync(request.BankAccountID).ConfigureAwait(false);

        if (bankAccount == null || bankAccount.BankAccountID == Constants.UnknownRecordID)
            return Result<BankAccount>.NotFound();

        return Result<BankAccount>.Success(bankAccount);
    }
}
