using AdminAssistant.DomainModel.Shared;
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void AddAdminAssistantWebAPIClient(this IServiceCollection services, ConfigurationSettings configurationSettings)
        => AddAdminAssistantWebAPIClient(services, new Uri(configurationSettings.WebApiClientBaseAddress));

    public static void AddAdminAssistantWebAPIClient(this IServiceCollection services, Uri baseAddress)
    {
        //services.AddHttpClient<IAdminAssistantWebAPIClient, AdminAssistantWebAPIClient>(AdminAssistant.Constants.AdminAssistantWebAPIClient, (httpClient) => httpClient.BaseAddress = baseAddress);
        //services.AddAutoMapper(typeof(MappingProfile));
    }
}
