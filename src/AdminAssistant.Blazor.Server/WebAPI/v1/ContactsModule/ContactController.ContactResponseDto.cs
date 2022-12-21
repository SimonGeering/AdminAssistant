using AdminAssistant.DomainModel.Modules.ContactsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.ContactsModule;

public record ContactResponseDto : IMapFrom<Contact>
{
    public int ContactID { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
}
