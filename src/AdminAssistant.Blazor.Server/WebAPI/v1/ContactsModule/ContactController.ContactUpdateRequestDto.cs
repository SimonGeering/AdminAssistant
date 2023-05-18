using AdminAssistant.DomainModel.Modules.ContactsModule;
using AdminAssistant.Framework.TypeMapping;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.ContactsModule;

[SwaggerSchema(Required = new[] { "ContactID", "FirstName", "LastName" })]
public sealed record ContactUpdateRequestDto : IMapTo<Contact>
{
    [SwaggerSchema("The Contact identifier.")]
    public int ContactID { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
}
