using AutoMapper;

namespace SimonGeering.Framework.TypeMapping;

/// <summary>Maps from the implementing type to the given type.</summary>
/// <typeparam name="TDestination">The type to map to.</typeparam>
public interface IMapTo<TDestination>
{
    void MapTo(Profile profile) => profile.CreateMap(GetType(), typeof(TDestination));
}
