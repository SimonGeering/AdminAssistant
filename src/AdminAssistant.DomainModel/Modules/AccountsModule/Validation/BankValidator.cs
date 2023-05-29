namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

internal sealed class BankValidator : AbstractValidator<Bank>, IBankValidator
{
    public BankValidator()
        => RuleFor(x => x.BankName)
            .NotEmpty()
            .MaximumLength(Bank.BankNameMaxLength);
}
