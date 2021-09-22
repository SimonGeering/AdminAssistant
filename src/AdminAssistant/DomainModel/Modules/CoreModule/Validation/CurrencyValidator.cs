using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.CoreModule.Validation;

public interface ICurrencyValidator : IValidator<Currency>
{
}
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
