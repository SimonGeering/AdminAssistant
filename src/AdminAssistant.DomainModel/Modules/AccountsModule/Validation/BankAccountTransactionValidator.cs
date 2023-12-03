namespace AdminAssistant.Modules.AccountsModule.Validation;

public interface IBankAccountTransactionValidator : IValidator<BankAccountTransaction>
{
}
internal sealed class BankAccountTransactionValidator : AbstractValidator<BankAccountTransaction>, IBankAccountTransactionValidator
{
    public BankAccountTransactionValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(BankAccountTransaction.DescriptionMaxLength);

        RuleFor(x => x.BankAccountID)
            .NotEqual(BankAccountId.Default);
    }
}
