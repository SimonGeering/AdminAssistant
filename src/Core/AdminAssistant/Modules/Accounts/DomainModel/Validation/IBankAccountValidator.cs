using FluentValidation;

namespace AdminAssistant.Accounts.DomainModel.Validation
{
    public interface IBankAccountValidator : IValidator<BankAccount>
    {
    }
}
