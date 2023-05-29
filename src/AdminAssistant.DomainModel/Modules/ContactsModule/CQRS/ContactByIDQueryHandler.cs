using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using AdminAssistant.Infra.DAL.Modules.ContactsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.DomainModel.Modules.ContactsModule.CQRS;

internal sealed class ContactByIDQueryHandler : RequestHandlerBase<ContactByIDQuery, Result<Contact>>
{
    private readonly IContactRepository _contactRepository;

    public ContactByIDQueryHandler(ILoggingProvider loggingProvider, IContactRepository contactRepository)
        : base(loggingProvider) => _contactRepository = contactRepository;

    public override async Task<Result<Contact>> Handle(ContactByIDQuery request, CancellationToken cancellationToken)
    {
        var result = await _contactRepository.GetAsync(request.ContactID).ConfigureAwait(false);
        if (result == null || result.ContactID == Constants.UnknownRecordID)
            return Result<Contact>.NotFound();

        return Result<Contact>.Success(result);
    }
}
