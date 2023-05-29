namespace AdminAssistant.DomainModel.Modules.BudgetModule.Validation;

internal sealed class BudgetValidator : AbstractValidator<Budget>, IBudgetValidator
{
    public BudgetValidator()
        => RuleFor(x => x.BudgetName)
            .NotEmpty()
            .MaximumLength(Budget.BudgetNameMaxLength);
}
