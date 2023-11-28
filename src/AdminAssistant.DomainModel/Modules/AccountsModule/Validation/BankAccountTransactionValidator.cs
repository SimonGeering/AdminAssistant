namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

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
