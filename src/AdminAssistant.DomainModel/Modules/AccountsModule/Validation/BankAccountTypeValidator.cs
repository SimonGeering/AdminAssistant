namespace AdminAssistant.Modules.AccountsModule.Validation;

public interface IBankAccountTypeValidator : IValidator<BankAccountType>
{
}
internal sealed class BankAccountTypeValidator : AbstractValidator<BankAccountType>, IBankAccountTypeValidator
{
    public BankAccountTypeValidator()
        => RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(BankAccountType.DescriptionMaxLength);
}

