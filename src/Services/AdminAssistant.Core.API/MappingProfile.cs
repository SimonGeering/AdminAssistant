using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.Core.API;

public class MappingProfile : MappingProfileBase
{
    public MappingProfile()
        : base(typeof(MappingProfile).Assembly)
    {
    }
}
