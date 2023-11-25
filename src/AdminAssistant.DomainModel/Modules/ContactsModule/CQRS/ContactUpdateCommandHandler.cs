using AdminAssistant.Infra.DAL.Modules.ContactsModule;
using AdminAssistant.DomainModel.Modules.ContactsModule.Validation;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.ContactsModule.CQRS;

internal sealed class ContactUpdateCommandHandler(
    ILoggingProvider loggingProvider,
    IContactRepository contactRepository,
    IContactValidator contactValidator)
    : RequestHandlerBase<ContactUpdateCommand, Result<Contact>>(loggingProvider)
{
    public override async Task<Result<Contact>> Handle(ContactUpdateCommand command, CancellationToken cancellationToken)
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
