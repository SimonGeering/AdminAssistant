namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

internal sealed class PayeeValidator : AbstractValidator<Payee>, IPayeeValidator
{
    public PayeeValidator()
        => RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Payee.NameMaxLength);
}
