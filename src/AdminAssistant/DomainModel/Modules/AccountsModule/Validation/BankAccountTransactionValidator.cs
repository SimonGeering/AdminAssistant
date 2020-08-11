using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation
{
    public interface IBankAccountTransactionValidator : IValidator<BankAccountTransaction>
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Fluent Validators are never used as collections directly")]
    internal class BankAccountTransactionValidator : AbstractValidator<BankAccountTransaction>, IBankAccountTransactionValidator
    {
        public BankAccountTransactionValidator()
        {
            this.RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(BankAccountTransaction.DescriptionMaxLength);

            this.RuleFor(x => x.BankAccountID)
                .NotEqual(Constants.UnknownRecordID);
        }
    }
}
