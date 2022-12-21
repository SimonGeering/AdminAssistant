namespace AdminAssistant.DomainModel.Modules.ContactsModule.CQRS;

public record ContactQuery : IRequest<Result<IEnumerable<Contact>>>;
public record ContactByIDQuery(int ContactID) : IRequest<Result<Contact>>;    
