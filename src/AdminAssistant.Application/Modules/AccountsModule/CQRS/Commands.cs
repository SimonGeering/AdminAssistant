using AdminAssistant.DomainModel.Modules.AccountsModule;

namespace AdminAssistant.Application.Modules.AccountsModule.CQRS
{
    public sealed record BankAccountCreateCommand(BankAccount BankAccount) : IRequest<Result<BankAccount>>;
    public sealed record BankAccountUpdateCommand(BankAccount BankAccount) : IRequest<Result<BankAccount>>;
    public sealed record BankCreateCommand(Bank Bank) : IRequest<Result<Bank>>;
    public sealed record BankUpdateCommand(Bank Bank) : IRequest<Result<Bank>>;
}
