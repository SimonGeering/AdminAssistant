using SimonGeering.Framework.TypeMapping;

namespace AdminAssistant.Services.Core;

public sealed class MappingProfile : MappingProfileBase
{
    public MappingProfile()
        : base(typeof(MappingProfile).Assembly)
    {
    }
}
