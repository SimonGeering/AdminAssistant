using AdminAssistant.Shared;
using SimonGeering.Framework.TypeMapping;

namespace AdminAssistant.Domain;

public class MappingProfile : MappingProfileBase
{
    public MappingProfile()
        : base(typeof(MappingProfile).Assembly)
    {
        CreateMap<EntityName, string>().ConvertUsing(x => x.Value);
        CreateMap<EntityDescription, string>().ConvertUsing(x => x.Value);
    }
}
