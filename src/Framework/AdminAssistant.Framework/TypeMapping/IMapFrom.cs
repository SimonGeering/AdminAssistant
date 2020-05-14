using AutoMapper;

namespace AdminAssistant.Framework.TypeMapping
{
    /// <summary>Maps from the given type to the implementing type.</summary>
    /// <typeparam name="TSource">The type to map from.</typeparam>
    public interface IMapFrom<TSource>
    {
        void MapFrom(Profile profile) => profile.CreateMap(typeof(TSource), GetType());
    }
}
