using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.Accounts.Validation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Fluent Validators are never used as collections directly")]
    public class BankAccountValidator : AbstractValidator<BankAccount>, IBankAccountValidator
    {
        public BankAccountValidator()
        {
            this.RuleFor(x => x.AccountName)
                .NotEmpty()
                .MaximumLength(BankAccount.AccountNameMaxLength);

            this.RuleFor(x => x.BankAccountTypeID)
                .NotEqual(Constants.UnknownRecordID);

            this.RuleFor(x => x.CurrencyID)
                .NotEqual(Constants.UnknownRecordID);

            // TODO: Validate BankAccountTypeID selection

            this.RuleFor(x => x.OpenedOn)
                .NotNull();
        }
    }
}
