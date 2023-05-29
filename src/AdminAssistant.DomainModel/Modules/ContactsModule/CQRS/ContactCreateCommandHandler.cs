using AdminAssistant.Infra.DAL.Modules.ContactsModule;
using AdminAssistant.DomainModel.Modules.ContactsModule.Validation;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.ContactsModule.CQRS;

internal sealed class ContactCreateCommandHandler : RequestHandlerBase<ContactCreateCommand, Result<Contact>>
{
    private readonly IContactRepository _contactRepository;
    private readonly IContactValidator _contactValidator;

    public ContactCreateCommandHandler(ILoggingProvider loggingProvider, IContactRepository contactRepository, IContactValidator contactValidator)
        : base(loggingProvider)
    {
        _contactRepository = contactRepository;
        _contactValidator = contactValidator;
    }

    public override async Task<Result<Contact>> Handle(ContactCreateCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await _contactValidator.ValidateAsync(command.Contact, cancellationToken).ConfigureAwait(false);

        if (validationResult.IsValid == false)
        {
            return Result<Contact>.Invalid(validationResult.AsErrors());
        }

        var result = await _contactRepository.SaveAsync(command.Contact).ConfigureAwait(false);
        return Result<Contact>.Success(result);
    }
}
