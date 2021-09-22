using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1;

public class MappingProfile : MappingProfileBase
{
    public MappingProfile()
        : base(typeof(MappingProfile).Assembly)
    {
    }
}
