using FluentValidation;

namespace AdminAssistant.Accounts.DomainModel.Validation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Fluent Validators are never used as collections directly")]
    public class BankAccountValidator : AbstractValidator<BankAccount>, IBankAccountValidator
    {
        public BankAccountValidator(ICurrencyValidator currencyValidator)
        {
            this.RuleFor(x => x.AccountName)
                .NotEmpty()
                .MaximumLength(BankAccount.AccountNameMaxLength);

            this.RuleFor(x => x.Currency)
                .NotNull()
                .SetValidator(currencyValidator);

            this.RuleFor(x => x.OpenedOn)
                .NotNull();
        }
    }
}
