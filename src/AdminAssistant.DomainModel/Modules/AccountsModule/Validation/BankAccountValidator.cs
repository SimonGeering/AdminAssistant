namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

internal sealed class BankAccountValidator : AbstractValidator<BankAccount>, IBankAccountValidator
{
    public BankAccountValidator()
    {
        RuleFor(x => x.AccountName)
            .NotEmpty()
            .MaximumLength(BankAccount.AccountNameMaxLength);

        RuleFor(x => x.BankAccountTypeID)
            .NotEqual(Constants.UnknownRecordID);

        RuleFor(x => x.CurrencyID)
            .NotEqual(Constants.UnknownRecordID);

        // TODO: Validate BankAccountTypeID selection against DB.

        RuleFor(x => x.OpenedOn)
            .NotNull();
    }
}
