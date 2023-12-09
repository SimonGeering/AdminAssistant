using SimonGeering.Framework.Helpers;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void AddSimonGeeringFramework(this IServiceCollection services)
        => services.AddTransient<IAssemblyAttributeHelper, AssemblyAttributeHelper>();
}
