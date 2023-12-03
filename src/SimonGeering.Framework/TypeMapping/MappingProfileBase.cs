using SimonGeering.Framework.Primitives;
using AutoMapper;
using System.Reflection;

namespace SimonGeering.Framework.TypeMapping;

public abstract class MappingProfileBase : Profile
{
    /// <summary>Based on 'https://github.com/jasontaylordev/CleanArchitecture/blob/master/src/Application/Common/Mappings/IMapFrom.cs'</summary>
    /// <param name="assembly"></param>
    protected MappingProfileBase(Assembly assembly)
    {
        ApplyIMapFromMappings(assembly);
        ApplyIMapToMappings(assembly);

        CreateMap<Id, int>().ConvertUsing(x => x.Value);
    }

    private void ApplyIMapFromMappings(Assembly assembly)
    {
        var mapFromTypes = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in mapFromTypes)
        {
            var instance = Activator.CreateInstance(type) ?? throw new TypeMappingException($"Unable to load type mapping from assembly {assembly.FullName}");
            MethodInfo? methodInfo;

            if (type.GetMethod("MapFrom") is null)
            {
                var mapFromInterface = type.GetInterface("IMapFrom`1") ?? throw new TypeMappingException($"Unable to load type mapping IMapFrom interface from type '{type.FullName}'");
                methodInfo = mapFromInterface.GetMethod("MapFrom");
            }
            else
            {
                methodInfo = type.GetMethod("MapFrom");
            }

            if (methodInfo is null)
                throw new TypeMappingException($"Unable to load MapFrom type mapping from type '{type.FullName}'");

            methodInfo.Invoke(instance, new object[] { this });
        }
    }

    private void ApplyIMapToMappings(Assembly assembly)
    {
        var mapToTypes = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>)))
            .ToList();

        foreach (var type in mapToTypes)
        {
            var instance = Activator.CreateInstance(type) ?? throw new TypeMappingException($"Unable to load type mapping from assembly {assembly.FullName}");
            MethodInfo? methodInfo;

            if (type.GetMethod("MapTo") is null)
            {
                var mapToInterface = type.GetInterface("IMapTo`1") ?? throw new TypeMappingException($"Unable to load type mapping IMapTo interface from type '{type.FullName}'");
                methodInfo = mapToInterface.GetMethod("MapTo");
            }
            else
            {
                methodInfo = type.GetMethod("MapTo");
            }

            if (methodInfo is null)
                throw new TypeMappingException($"Unable to load MapTo type mapping from type '{type.FullName}'");

            methodInfo.Invoke(instance, new object[] { this });
        }
    }
}
