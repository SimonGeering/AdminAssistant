namespace AdminAssistant.DomainModel.Modules.ContactsModule.Validation;

internal sealed class ContactValidator : AbstractValidator<Contact>, IContactValidator
{
    public ContactValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty();
        RuleFor(x => x.FirstName)
            .MaximumLength(Contact.ContactFirstNameMaxLength);

        RuleFor(x => x.LastName)
            .NotEmpty();
        RuleFor(x => x.LastName)
            .MaximumLength(Contact.ContactLastNameMaxLength);
    }
}
