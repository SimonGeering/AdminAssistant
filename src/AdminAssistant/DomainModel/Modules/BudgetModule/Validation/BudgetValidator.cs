using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.BudgetModule.Validation
{
    public interface IBudgetValidator : IValidator<Budget>
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class BudgetValidator : AbstractValidator<Budget>, IBudgetValidator
    {
        public BudgetValidator()
        {
            RuleFor(x => x.BudgetName)
                .NotEmpty();
            RuleFor(x => x.BudgetName)
                .MaximumLength(Budget.BudgetNameMaxLength);
        }
    }
}
