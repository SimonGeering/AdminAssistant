namespace AdminAssistant.DomainModel.Modules.ContactsModule.Validation;

internal sealed class ContactValidator : AbstractValidator<Contact>, IContactValidator
{
    public ContactValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(Contact.FirstNameMaxLength);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(Contact.LastNameMaxLength);
    }
}
