using AdminAssistant;
using AdminAssistant.Application;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjectionExtensions
{
    public static IServiceCollection AddAdminAssistantApplication(this IServiceCollection services)
    {
        services.AddMediator((MediatorOptions options) =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
            options.Assemblies = [typeof(RequestHandlerBase<,>)];
            options.PipelineBehaviors = [typeof(LoggingPipelineBehaviour<,>)];
        });
        return services;
    }
}
