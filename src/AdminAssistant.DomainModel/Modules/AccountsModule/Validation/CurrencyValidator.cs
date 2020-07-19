using FluentValidation;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Fluent Validators are never used as collections directly")]
    public class CurrencyValidator : AbstractValidator<Currency>, ICurrencyValidator
    {
        public CurrencyValidator()
        {
            this.RuleFor(x => x.CurrencyID)
                .GreaterThanOrEqualTo(Constants.UnknownRecordID);

            this.RuleFor(x => x.DecimalFormat)
                .NotEmpty()
                .MaximumLength(Currency.DecimalFormatMaxLength);

            this.RuleFor(x => x.Symbol)
                .NotEmpty()
                .MaximumLength(Currency.SymbolMaxLength);
        }
    }
}
