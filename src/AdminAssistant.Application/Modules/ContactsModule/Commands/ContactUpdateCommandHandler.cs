using AdminAssistant.Modules.ContactsModule.Infrastructure.DAL;
using AdminAssistant.Modules.ContactsModule.Validation;

namespace AdminAssistant.Modules.ContactsModule.Commands;

public record ContactUpdateCommand(Contact Contact) : IRequest<Result<Contact>>;

internal sealed class ContactUpdateCommandHandler(
    ILoggingProvider loggingProvider,
    IContactRepository contactRepository,
    IContactValidator contactValidator)
    : RequestHandlerBase<ContactUpdateCommand, Result<Contact>>(loggingProvider)
{
    public override async ValueTask<Result<Contact>> Handle(ContactUpdateCommand command, CancellationToken cancellationToken)
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
