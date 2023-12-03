using AdminAssistant.Modules.ContactsModule;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.ContactsModule;

[SwaggerSchema(Required = new[] { "ContactID", "FirstName", "LastName" })]
public sealed record ContactUpdateRequestDto : IMapTo<Contact>
{
    [SwaggerSchema("The Contact identifier.")]
    public int ContactID { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;

    public void MapTo(AutoMapper.Profile profile) => profile
        .CreateMap<ContactUpdateRequestDto, Contact>()
        .ForMember(x => x.OwnerID, opt => opt.Ignore())
        .ForMember(x => x.TitleID, opt => opt.Ignore())
        .ForMember(x => x.DateOfBirth, opt => opt.Ignore());
}
