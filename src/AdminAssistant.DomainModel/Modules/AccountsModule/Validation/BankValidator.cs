namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

internal class BankValidator : AbstractValidator<Bank>, IBankValidator
{
    public BankValidator()
    {
        RuleFor(x => x.BankName)
            .NotEmpty();
        RuleFor(x => x.BankName)
            .MaximumLength(Bank.BankNameMaxLength);
    }
}
