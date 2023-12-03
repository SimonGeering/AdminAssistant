using AdminAssistant.Modules.CoreModule;

namespace AdminAssistant.Modules.AccountsModule.Validation;

public interface IBankAccountValidator : IValidator<BankAccount>
{
}
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
