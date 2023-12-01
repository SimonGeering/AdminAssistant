namespace AdminAssistant.Modules.BudgetModule.Validation;

public interface IBudgetValidator : IValidator<Budget>
{
}
internal sealed class BudgetValidator : AbstractValidator<Budget>, IBudgetValidator
{
    public BudgetValidator()
        => RuleFor(x => x.BudgetName)
            .NotEmpty()
            .MaximumLength(Budget.BudgetNameMaxLength);
}
