using AdminAssistant.Infrastructure.EntityFramework.Model.Contacts;

namespace AdminAssistant.Modules.ContactsModule;

public static class ContactsModuleMapper
{
    public static List<Contact> ToContactList(this List<ContactEntity> entities)
        => entities.Select(ToContact).ToList();

    public static ContactEntity ToContactEntity(this Contact domainObject)
        => new()
        {
            ContactID = domainObject.ContactID.Value,
            OwnerID = domainObject.OwnerID,
            TitleID = domainObject.TitleID,
            FirstName = domainObject.FirstName,
            LastName = domainObject.LastName,
            DateOfBirth = domainObject.DateOfBirth
        };

    public static List<ContactEntity> ToContactEntityList(this List<Contact> domainObjects)
        => domainObjects.Select(ToContactEntity).ToList();

    public static Contact ToContact(this ContactEntity entity)
        => new()
        {
            ContactID = new ContactId(entity.ContactID),
            OwnerID = entity.OwnerID,
            TitleID = entity.TitleID,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            DateOfBirth = entity.DateOfBirth
        };
}
