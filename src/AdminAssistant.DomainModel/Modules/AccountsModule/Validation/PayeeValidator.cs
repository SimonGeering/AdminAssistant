namespace AdminAssistant.Modules.AccountsModule.Validation;

public interface IPayeeValidator : IValidator<Payee>
{
}
internal sealed class PayeeValidator : AbstractValidator<Payee>, IPayeeValidator
{
    public PayeeValidator()
        => RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Payee.NameMaxLength);
}
