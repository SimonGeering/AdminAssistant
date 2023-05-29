namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

internal sealed class BankAccountTypeValidator : AbstractValidator<BankAccountType>, IBankAccountTypeValidator
{
    public BankAccountTypeValidator()
        => RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(BankAccountType.DescriptionMaxLength);
}

