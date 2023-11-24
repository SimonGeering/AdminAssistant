namespace AdminAssistant.DomainModel.Modules.ContactsModule.CQRS;

public record ContactCreateCommand(Contact Contact) : IRequest<Result<Contact>>;
public record ContactUpdateCommand(Contact Contact) : IRequest<Result<Contact>>;
