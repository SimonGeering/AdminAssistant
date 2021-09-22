using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.Infra.DAL;

public class MappingProfile : MappingProfileBase
{
    public MappingProfile()
        : base(typeof(MappingProfile).Assembly)
    {
    }
}
