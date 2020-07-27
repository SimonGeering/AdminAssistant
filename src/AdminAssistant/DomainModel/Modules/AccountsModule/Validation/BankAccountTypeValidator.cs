using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation
{
    public interface IBankAccountTypeValidator : IValidator<BankAccountType>
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Fluent Validators are never used as collections directly")]
    internal class BankAccountTypeValidator : AbstractValidator<BankAccountType>, IBankAccountTypeValidator
    {
        public BankAccountTypeValidator()
        {
            this.RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(BankAccountType.DescriptionMaxLength);
        }
    }
}

