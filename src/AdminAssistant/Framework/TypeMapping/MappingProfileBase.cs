using AutoMapper;
using System.Reflection;

namespace AdminAssistant.Framework.TypeMapping
{
    public abstract class MappingProfileBase : Profile
    {
        /// <summary>Based on 'https://github.com/jasontaylordev/CleanArchitecture/blob/master/src/Application/Common/Mappings/IMapFrom.cs'</summary>
        /// <param name="assembly"></param>
        public MappingProfileBase(Assembly assembly)
        {
            ApplyIMapFromMappings(assembly);
            ApplyIMapToMappings(assembly);
        }

        private void ApplyIMapFromMappings(Assembly assembly)
        {
            var mapFromTypes = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in mapFromTypes)
            {
                var instance = Activator.CreateInstance(type);

                if (instance is null)
                    throw new ArgumentException("Unable to load type mapping from assembly", nameof(assembly));

                MethodInfo? methodInfo;

                if (type.GetMethod("MapFrom") is null)
                {
                    var mapfromInterface = type.GetInterface("IMapFrom`1");

                    if (mapfromInterface is null)
                        throw new NullReferenceException($"Unable to load type mapping IMapFrom interface from type '{type.FullName}'");

                    methodInfo = mapfromInterface.GetMethod("MapFrom");
                }
                else
                {
                    methodInfo = type.GetMethod("MapFrom");
                }

                if (methodInfo is null)
                    throw new NullReferenceException($"Unable to load MapFrom type mapping from type '{type.FullName}'");

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
                var instance = Activator.CreateInstance(type);

                if (instance is null)
                    throw new ArgumentException("Unable to load type mapping from assembly", nameof(assembly));

                MethodInfo? methodInfo;

                if (type.GetMethod("MapTo") is null)
                {
                    var maptoInterface = type.GetInterface("IMapTo`1");

                    if (maptoInterface is null)
                        throw new NullReferenceException($"Unable to load type mapping IMapTo interface from type '{type.FullName}'");

                    methodInfo = maptoInterface.GetMethod("MapTo");
                }
                else
                {
                    methodInfo = type.GetMethod("MapTo");
                }

                if (methodInfo is null)
                    throw new NullReferenceException($"Unable to load MapTo type mapping from type '{type.FullName}'");

                methodInfo.Invoke(instance, new object[] { this });
            }
        }
    }
}
