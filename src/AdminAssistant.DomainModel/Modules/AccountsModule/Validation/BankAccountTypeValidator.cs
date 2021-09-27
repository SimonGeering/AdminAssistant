namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

internal class BankAccountTypeValidator : AbstractValidator<BankAccountType>, IBankAccountTypeValidator
{
    public BankAccountTypeValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty();
        RuleFor(x => x.Description)
            .MaximumLength(BankAccountType.DescriptionMaxLength);
    }
}

