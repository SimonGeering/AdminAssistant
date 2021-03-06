using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation
{
    public interface IBankAccountTypeValidator : IValidator<BankAccountType>
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
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
}

