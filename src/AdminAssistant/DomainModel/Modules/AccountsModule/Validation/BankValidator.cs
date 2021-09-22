using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

public interface IBankValidator : IValidator<Bank>
{
}
internal class BankValidator : AbstractValidator<Bank>, IBankValidator
{
    public BankValidator()
    {
        RuleFor(x => x.BankName)
            .NotEmpty();
        RuleFor(x => x.BankName)
            .MaximumLength(Bank.BankNameMaxLength);
    }
}
