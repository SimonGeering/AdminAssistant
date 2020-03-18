using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.Accounts.Validation
{
    public interface IBankAccountValidator : IValidator<BankAccount>
    {
    }
}
