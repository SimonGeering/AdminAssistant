using AdminAssistant.DomainModel.Modules.ContactsModule;
using AdminAssistant.Framework.TypeMapping;
using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.ContactsModule;

[SwaggerSchema(Required = new[] { "FirstName", "LastName" })]
public sealed record ContactCreateRequestDto : IMapTo<Contact>
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;

    public void MapTo(AutoMapper.Profile profile)=> profile
        .CreateMap<ContactCreateRequestDto, Contact>()
        .ForMember(x => x.ContactID, opt => opt.Ignore())
        .ForMember(x => x.OwnerID, opt => opt.Ignore())
        .ForMember(x => x.TitleID, opt => opt.Ignore())
        .ForMember(x => x.DateOfBirth, opt => opt.Ignore());
}
