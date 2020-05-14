using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI
{
    public class MappingProfile : MappingProfileBase
    {
        public MappingProfile()
            : base(typeof(MappingProfile).Assembly)
        {
        }
    }
}
