using AdminAssistant.Modules.ContactsModule;

namespace AdminAssistant.Infra.DAL.Modules.ContactsModule;

public interface IContactRepository : IRepository<Contact, ContactId>
{
}
