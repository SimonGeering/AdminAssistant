using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Fluent Validators are never used as collections directly")]
    internal class BankAccountValidator : AbstractValidator<BankAccount>, IBankAccountValidator
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
