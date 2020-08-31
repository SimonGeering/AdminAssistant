using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation
{
    public interface ICurrencyValidator : IValidator<Currency>
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Build", "CA1812", Justification = "Compiler dosen't understand dependency injection")]
    internal class CurrencyValidator : AbstractValidator<Currency>, ICurrencyValidator
    {
        public CurrencyValidator()
        {
            RuleFor(x => x.DecimalFormat)
                .NotEmpty()
                .MaximumLength(Currency.DecimalFormatMaxLength);

            RuleFor(x => x.Symbol)
                .NotEmpty()
                .MaximumLength(Currency.SymbolMaxLength);
        }
    }
}
