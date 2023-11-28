using AdminAssistant.DomainModel.Modules.CoreModule;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

internal sealed class BankAccountValidator : AbstractValidator<BankAccount>, IBankAccountValidator
{
    public BankAccountValidator()
    {
        RuleFor(x => x.AccountName)
            .NotEmpty()
            .MaximumLength(BankAccount.AccountNameMaxLength);

        RuleFor(x => x.BankAccountTypeID)
            .NotEqual(BankAccountTypeId.Default);

        RuleFor(x => x.CurrencyID)
            .NotEqual(CurrencyId.Default);

        // TODO: Validate BankAccountTypeID selection against DB.

        RuleFor(x => x.OpenedOn)
            .NotNull();
    }
}
