namespace AdminAssistant.Modules.CoreModule.Validation;

public interface ICurrencyValidator : IValidator<Currency>
{
}
internal sealed class CurrencyValidator : AbstractValidator<Currency>, ICurrencyValidator
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
