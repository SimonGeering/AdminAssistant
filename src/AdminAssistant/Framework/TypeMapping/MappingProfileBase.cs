using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace AdminAssistant.Framework.TypeMapping
{
    public abstract class MappingProfileBase : Profile
    {
        /// <summary>Based on 'https://github.com/jasontaylordev/CleanArchitecture/blob/master/src/Application/Common/Mappings/IMapFrom.cs'</summary>
        /// <param name="assembly"></param>
        public MappingProfileBase(Assembly assembly)
        {
            this.ApplyIMapFromMappings(assembly);
            this.ApplyIMapToMappings(assembly);
        }

        private void ApplyIMapFromMappings(Assembly assembly)
        {
            var mapFromTypes = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in mapFromTypes)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("MapFrom")
                    ?? type.GetInterface("IMapFrom`1").GetMethod("MapFrom");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }

        private void ApplyIMapToMappings(Assembly assembly)
        {
            var mapToTypes = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>)))
                .ToList();

            foreach (var type in mapToTypes)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("MapTo")
                    ?? type.GetInterface("IMapTo`1").GetMethod("MapTo");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
