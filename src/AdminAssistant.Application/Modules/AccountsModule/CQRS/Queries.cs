using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.Application.Modules.AccountsModule.CQRS
{
    public sealed record BankAccountByIDQuery(int BankAccountID) : IRequest<Result<BankAccount>>;
    public sealed record BankAccountInfoQuery(int OwnerID) : IRequest<Result<IEnumerable<BankAccountInfo>>>;
    public sealed record BankAccountTypesQuery : IRequest<Result<IEnumerable<BankAccountType>>>;
    public sealed record BankAccountTransactionsByBankAccountIDQuery(int BankAccountID) : IRequest<Result<IEnumerable<BankAccountTransaction>>>;
    public sealed record BankByIDQuery(int BankID) : IRequest<Result<Bank>>;
    public sealed record BankQuery : IRequest<Result<IEnumerable<Bank>>>;
}
