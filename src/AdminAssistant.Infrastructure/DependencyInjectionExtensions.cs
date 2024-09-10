using AdminAssistant;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.MediatR;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;
using AdminAssistant.Modules.ContactsModule.Infrastructure.DAL;
using AdminAssistant.Modules.CoreModule.Infrastructure.DAL;
using AdminAssistant.Modules.DocumentsModule.Infrastructure.DAL;
using MediatR;
using Microsoft.Extensions.Hosting;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("AdminAssistant.Test")]

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void AddAdminAssistantServerSideInfra(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>)); // Use typeof because <,>

        AddAccountsDAL(services);
        AddContactsDAL(services);
        AddCoreDAL(services);
        AddDocumentsDAL(services);
    }

    public static void AddAdminAssistantApplicationDbContext(this IHostApplicationBuilder builder)
        => builder.AddSqlServerDbContext<ApplicationDbContext>(Constants.ApplicationDatabaseName);

    public static void AddAdminAssistantServerSideProviders(this IServiceCollection services)
    {
        services.AddTransient<ILoggingProvider, ServerSideLoggingProvider>();
        services.AddSharedProviders();
    }

    private static void AddAccountsDAL(this IServiceCollection services)
    {
        services.AddTransient<IBankAccountInfoRepository, BankAccountInfoRepository>();
        services.AddTransient<IBankAccountRepository, BankAccountRepository>();
        services.AddTransient<IBankAccountTransactionRepository, BankAccountTransactionRepository>();
        services.AddTransient<IBankAccountTypeRepository, BankAccountTypeRepository>();
        services.AddTransient<IBankRepository, BankRepository>();
    }

    private static void AddContactsDAL(this IServiceCollection services)
        => services.AddTransient<IContactRepository, ContactRepository>();

    private static void AddCoreDAL(this IServiceCollection services)
        => services.AddTransient<ICurrencyRepository, CurrencyRepository>();

    private static void AddDocumentsDAL(this IServiceCollection services)
        => services.AddTransient<IDocumentRepository, DocumentRepository>();
}
