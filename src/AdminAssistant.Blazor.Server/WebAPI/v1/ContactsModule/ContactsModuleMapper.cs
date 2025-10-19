using AdminAssistant.Modules.ContactsModule;

namespace AdminAssistant.WebAPI.v1.ContactsModule;

public static class ContactsModuleMapper
{
    public static Contact ToContact(this ContactCreateRequestDto source)
        => new()
        {
            ContactID = ContactId.Default,
            FirstName = source.FirstName,
            LastName = source.LastName,
        };

    public static Contact ToContact(this ContactUpdateRequestDto source)
        => new()
        {
            ContactID = new ContactId(source.ContactID),
            FirstName = source.FirstName,
            LastName = source.LastName,
        };

    public static ContactResponseDto ToContactResponseDto(this Contact source)
        => new()
        {
            ContactID = source.ContactID.Value,
            FirstName = source.FirstName,
            LastName = source.LastName,
        };

    public static IEnumerable<ContactResponseDto> ToContactResponseDtoEnumeration(this IEnumerable<Contact> source)
        => source.Select(x => new ContactResponseDto
        {
            ContactID = x.ContactID.Value,
            FirstName = x.FirstName,
            LastName = x.LastName,
        });
}
