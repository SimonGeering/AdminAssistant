using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1;

public sealed class MappingProfile : MappingProfileBase
{
    public MappingProfile()
        : base(typeof(MappingProfile).Assembly)
    {
    }
}
