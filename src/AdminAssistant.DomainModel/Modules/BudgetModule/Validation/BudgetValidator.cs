namespace AdminAssistant.DomainModel.Modules.BudgetModule.Validation;

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