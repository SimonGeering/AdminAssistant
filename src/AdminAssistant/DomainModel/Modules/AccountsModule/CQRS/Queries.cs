namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public record BankAccountByIDQuery(int BankAccountID) : IRequest<Result<BankAccount>>;
    public record BankAccountInfoQuery(int OwnerID) : IRequest<Result<IEnumerable<BankAccountInfo>>>;
    public record BankAccountTypesQuery : IRequest<Result<IEnumerable<BankAccountType>>>;
    public record BankAccountTransactionsByBankAccountIDQuery(int BankAccountID) : IRequest<Result<IEnumerable<BankAccountTransaction>>>;
    public record BankByIDQuery(int BankID) : IRequest<Result<Bank>>;
    public record BankQuery : IRequest<Result<IEnumerable<Bank>>>;
}
