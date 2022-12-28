namespace AdminAssistant.DomainModel.Modules.CoreModule.Validation;

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
