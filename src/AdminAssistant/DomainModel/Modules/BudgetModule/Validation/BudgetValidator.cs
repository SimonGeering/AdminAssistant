using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.BudgetModule.Validation;

public interface IBudgetValidator : IValidator<Budget>
{
}
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
