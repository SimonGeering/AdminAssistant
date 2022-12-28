namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

internal sealed class BankAccountValidator : AbstractValidator<BankAccount>, IBankAccountValidator
{
    //private bool BankAccountBeingCreated(CustomContext context) => context.ParentContext.RootContextData.ContainsKey(Constants.IsCreateCommandContext);

    public BankAccountValidator()
    {
        // Keep this as an example of how to do custom context incase we need it elsewhere later
        // However this is not needed as it is not a Business Rule violation since the user can't
        // do anything to fix it.
        // Instead this is enforced in the command with a GuardClause so an exception will fail early.
        //
        //this.RuleFor(x => x.BankAccountID)
        //    .Custom((bankAccountID, context) =>
        //    { 
        //        if (BankAccountBeingCreated(context))
        //        {
        //            if (bankAccountID != Constants.NewRecordID)
        //                context.AddFailure("BankAccountID ");
        //        }
        //        else
        //        {
        //            if (bankAccountID <= Constants.NewRecordID)
        //                context.AddFailure("");
        //        }
        //    });

        RuleFor(x => x.AccountName)
            .NotEmpty()
            .MaximumLength(BankAccount.AccountNameMaxLength);

        RuleFor(x => x.BankAccountTypeID)
            .NotEqual(Constants.UnknownRecordID);

        RuleFor(x => x.CurrencyID)
            .NotEqual(Constants.UnknownRecordID);

        // TODO: Validate BankAccountTypeID selection

        RuleFor(x => x.OpenedOn)
            .NotNull();
    }
}
