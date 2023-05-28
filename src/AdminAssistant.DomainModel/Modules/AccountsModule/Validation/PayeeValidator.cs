namespace AdminAssistant.DomainModel.Modules.AccountsModule.Validation;

public sealed class PayeeValidator : AbstractValidator<Payee>, IPayeeValidator
{
    public PayeeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
        RuleFor(x => x.Name)
            .MaximumLength(Payee.NameMaxLength);
    }
}
