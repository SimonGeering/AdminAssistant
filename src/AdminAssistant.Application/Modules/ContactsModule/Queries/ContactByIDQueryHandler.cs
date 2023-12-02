using AdminAssistant.Infra.DAL.Modules.ContactsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.Modules.ContactsModule.Queries;

public sealed record ContactQuery : IRequest<Result<IEnumerable<Contact>>>;

public sealed record ContactByIDQuery(int ContactID) : IRequest<Result<Contact>>;

internal sealed class ContactByIDQueryHandler(ILoggingProvider loggingProvider, IContactRepository contactRepository)
    : RequestHandlerBase<ContactByIDQuery, Result<Contact>>(loggingProvider)
{
    public override async Task<Result<Contact>> Handle(ContactByIDQuery request, CancellationToken cancellationToken)
    {
        var result = await contactRepository.GetAsync(new ContactId(request.ContactID), cancellationToken).ConfigureAwait(false);
        if (result == null || result.ContactID.IsUnknownRecordID)
            return Result<Contact>.NotFound();

        return Result<Contact>.Success(result);
    }
}
