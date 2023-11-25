using AdminAssistant.Infra.DAL.Modules.ContactsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.ContactsModule.CQRS;

internal sealed class ContactByIDQueryHandler(ILoggingProvider loggingProvider, IContactRepository contactRepository)
    : RequestHandlerBase<ContactByIDQuery, Result<Contact>>(loggingProvider)
{
    public override async Task<Result<Contact>> Handle(ContactByIDQuery request, CancellationToken cancellationToken)
    {
        var result = await contactRepository.GetAsync(request.ContactID, cancellationToken).ConfigureAwait(false);
        if (result == null || result.ContactID == Constants.UnknownRecordID)
            return Result<Contact>.NotFound();

        return Result<Contact>.Success(result);
    }
}
