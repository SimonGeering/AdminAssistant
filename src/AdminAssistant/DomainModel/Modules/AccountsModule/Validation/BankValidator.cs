using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation
{    
    public interface IBankValidator : IValidator<Bank>
    {
    }

    [SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
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
}
