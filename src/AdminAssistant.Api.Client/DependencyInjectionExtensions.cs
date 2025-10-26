using AdminAssistant;
using AdminAssistant.WebAPIClient.v1.AccountsModule;
using AdminAssistant.WebAPIClient.v1.AccountsModule.AdminAssistant.WebAPIClient.v1.AccountsModule;
using AdminAssistant.WebAPIClient.v1.AssetRegisterModule;
using AdminAssistant.WebAPIClient.v1.BudgetModule;
using AdminAssistant.WebAPIClient.v1.CalendarModule;
using AdminAssistant.WebAPIClient.v1.ContactsModule;
using AdminAssistant.WebAPIClient.v1.CoreModule;
using AdminAssistant.WebAPIClient.v1.DocumentsModule;
using AdminAssistant.WebAPIClient.v1.MailModule;
using AdminAssistant.WebAPIClient.v1.TasksModule;
using AdminAssistant.WebAPIClient.v1.WeatherModule;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjectionExtensions
{
    public static IServiceCollection AddAdminAssistantApiClient(this IServiceCollection services)
    {
        var baseUri = new Uri($"https+http://{Constants.Api}");

        services.AddRefitClientWithBaseUri<IBankAccountApiClient>(baseUri);
        services.AddRefitClientWithBaseUri<IBankAccountInfoApiClient>(baseUri);
        services.AddRefitClientWithBaseUri<IBankAccountTypeApiClient>(baseUri);
        services.AddRefitClientWithBaseUri<IBankApiClient>(baseUri);
        services.AddRefitClientWithBaseUri<IAssetApiClient>(baseUri);
        services.AddRefitClientWithBaseUri<IBudgetApiClient>(baseUri);
        services.AddRefitClientWithBaseUri<IReminderApiClient>(baseUri);
        services.AddRefitClientWithBaseUri<IContactApiClient>(baseUri);
        services.AddRefitClientWithBaseUri<ICurrencyApiClient>(baseUri);
        services.AddRefitClientWithBaseUri<IDocumentApiClient>(baseUri);
        services.AddRefitClientWithBaseUri<IMailMessageApiClient>(baseUri);
        services.AddRefitClientWithBaseUri<ITaskListApiClient>(baseUri);
        services.AddRefitClientWithBaseUri<IWeatherForecastApiClient>(baseUri);

        return services;
    }

    private static IServiceCollection AddRefitClientWithBaseUri<TClient>(this IServiceCollection services, Uri baseUri)
        where TClient : class
    {
        services.AddRefitClient<TClient>().ConfigureHttpClient(c => c.BaseAddress = baseUri);
        return services;
    }
}
