namespace AdminAssistant.DomainModel.Modules.ContactsModule.CQRS
{
    public record ContactQuery : IRequest<Result<IEnumerable<Contact>>>;
}
