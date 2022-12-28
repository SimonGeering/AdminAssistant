namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public record BankAccountCreateCommand(BankAccount BankAccount) : IRequest<Result<BankAccount>>;
    public record BankAccountUpdateCommand(BankAccount BankAccount) : IRequest<Result<BankAccount>>;
    public record BankCreateCommand(Bank Bank) : IRequest<Result<Bank>>;
    public record BankUpdateCommand(Bank Bank) : IRequest<Result<Bank>>;
}
