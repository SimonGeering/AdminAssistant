using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation
{    
    public interface IBankValidator : IValidator<Bank>
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Fluent Validators are never used as collections directly")]
    internal class BankValidator : AbstractValidator<Bank>, IBankValidator
    {
        public BankValidator()
        {
            this.RuleFor(x => x.BankName)
                .NotEmpty()
                .MaximumLength(Bank.BankNameMaxLength);
        }
    }
}
