using AdminAssistant.DomainModel.Modules.ContactsModule;

namespace AdminAssistant.Application.Modules.ContactsModule.CQRS;

public sealed record ContactQuery : IRequest<Result<IEnumerable<Contact>>>;
public sealed record ContactByIDQuery(int ContactID) : IRequest<Result<Contact>>;    
