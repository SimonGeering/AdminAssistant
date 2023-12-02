using AdminAssistant.Infra.DAL.Modules.ContactsModule;
using AdminAssistant.Modules.ContactsModule.Validation;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.Modules.ContactsModule.Commands;

public record ContactCreateCommand(Contact Contact) : IRequest<Result<Contact>>;

internal sealed class ContactCreateCommandHandler(
    ILoggingProvider loggingProvider,
    IContactRepository contactRepository,
    IContactValidator contactValidator)
    : RequestHandlerBase<ContactCreateCommand, Result<Contact>>(loggingProvider)
{
    public override async Task<Result<Contact>> Handle(ContactCreateCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await contactValidator.ValidateAsync(command.Contact, cancellationToken).ConfigureAwait(false);

        if (validationResult.IsValid == false)
        {
            return Result<Contact>.Invalid(validationResult.AsErrors());
        }

        var result = await contactRepository.SaveAsync(command.Contact, cancellationToken).ConfigureAwait(false);
        return Result<Contact>.Success(result);
    }
}
