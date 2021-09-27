using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

public interface IBankAccountTransactionValidator : IValidator<BankAccountTransaction>
{
}
