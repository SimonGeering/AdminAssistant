using AdminAssistant.Shared.Validation;

namespace AdminAssistant.Modules.AccountsModule.Validation;

public interface IBankValidator : IValidator<Bank>
{
}
internal sealed class BankValidator : AbstractValidator<Bank>, IBankValidator
{
    public BankValidator()
        => RuleFor(x => x.BankName).ValidEntityName(nameof(Bank.BankName));
}
