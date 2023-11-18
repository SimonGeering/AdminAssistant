namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation
{
    public interface IBankValidator : IValidator<Bank> { }
    public interface IBankAccountValidator : IValidator<BankAccount> { }
    public interface IBankAccountTransactionValidator : IValidator<BankAccountTransaction> { }
    public interface IBankAccountTypeValidator : IValidator<BankAccountType> { }
    public interface IPayeeValidator : IValidator<Payee> { }
}
