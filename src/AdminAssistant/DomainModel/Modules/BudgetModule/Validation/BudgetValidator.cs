using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.BudgetModule.Validation
{
    public interface IBudgetValidator : IValidator<Budget>
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Fluent Validators are never used as collections directly")]
    internal class BudgetValidator : AbstractValidator<Budget>, IBudgetValidator
    {
        public BudgetValidator()
        {
            this.RuleFor(x => x.BudgetName)
                .NotEmpty()
                .MaximumLength(Budget.BudgetNameMaxLength);
        }
    }
}
