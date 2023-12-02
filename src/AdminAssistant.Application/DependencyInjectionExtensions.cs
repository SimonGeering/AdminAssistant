using Microsoft.Extensions.DependencyInjection;

namespace AdminAssistant;

public static partial class DependencyInjectionExtensions
{
    public static IServiceCollection AddAdminAssistantApplication(this IServiceCollection services)
    {
        // Set-up / Add MediatR based on an assembly marker type ...
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RequestHandlerBase<,>).Assembly));
        return services;
    }
}
