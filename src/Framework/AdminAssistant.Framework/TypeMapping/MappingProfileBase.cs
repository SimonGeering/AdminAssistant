using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace AdminAssistant.Framework.TypeMapping
{
    public abstract class MappingProfileBase : Profile
    {
        public MappingProfileBase(Assembly assembly)
        {
            this.ApplyMappingsFromAssembly(assembly);
        }

        /// <summary>Based on 'https://github.com/jasontaylordev/CleanArchitecture/blob/master/src/Application/Common/Mappings/IMapFrom.cs'</summary>
        /// <param name="assembly"></param>
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapping<,>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Map")
                    ?? type.GetInterface("IMapping`2").GetMethod("Map");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
