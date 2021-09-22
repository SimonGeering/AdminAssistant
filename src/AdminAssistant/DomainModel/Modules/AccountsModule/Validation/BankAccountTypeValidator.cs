using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

public interface IBankAccountTypeValidator : IValidator<BankAccountType>
{
}
internal class BankAccountTypeValidator : AbstractValidator<BankAccountType>, IBankAccountTypeValidator
{
    public BankAccountTypeValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty();
        RuleFor(x => x.Description)
            .MaximumLength(BankAccountType.DescriptionMaxLength);
    }
}

