using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation
{
    public interface IBankAccountTransactionValidator : IValidator<BankAccountTransaction>
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BankAccountTransactionValidator : AbstractValidator<BankAccountTransaction>, IBankAccountTransactionValidator
    {
        public BankAccountTransactionValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(BankAccountTransaction.DescriptionMaxLength);

            RuleFor(x => x.BankAccountID)
                .NotEqual(Constants.UnknownRecordID);
        }
    }
}
