using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.BudgetModule.Validation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Fluent Validators are never used as collections directly")]
    public class BudgetValidator : AbstractValidator<Budget>, IBudgetValidator
    {
        public BudgetValidator()
        {
            this.RuleFor(x => x.BudgetName)
                .NotEmpty();
        }
    }
}
