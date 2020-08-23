using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.DAL
{
    public class MappingProfile : MappingProfileBase
    {
        public MappingProfile()
            : base(typeof(MappingProfile).Assembly)
        {
        }
    }
}
