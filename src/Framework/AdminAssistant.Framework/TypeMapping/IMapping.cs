using AutoMapper;

namespace AdminAssistant.Framework.TypeMapping
{
    public interface IMapping<TSource, TDestination>
    {
        void Map(Profile profile) => profile.CreateMap(typeof(TSource), typeof(TDestination));
    }
}
