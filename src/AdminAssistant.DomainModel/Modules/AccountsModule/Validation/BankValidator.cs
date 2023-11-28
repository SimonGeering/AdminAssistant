using AdminAssistant.Abstractions.DomainModel.Shared.Validation;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

internal sealed class BankValidator : AbstractValidator<Bank>, IBankValidator
{
    public BankValidator(IEntityNameValidator entityNameValidator)
        => RuleFor(x => x.BankName).SetValidator(x => entityNameValidator).WithName("BankName");
}
