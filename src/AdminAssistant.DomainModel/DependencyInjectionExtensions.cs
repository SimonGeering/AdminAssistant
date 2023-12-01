using AdminAssistant.Modules.AccountsModule.Validation;
using AdminAssistant.Modules.BudgetModule.Validation;
using AdminAssistant.Modules.ContactsModule.Validation;
using AdminAssistant.Modules.CoreModule.Validation;
using AdminAssistant.Shared;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjectionExtensions
{
    public static IServiceCollection AddAdminAssistantClientSideDomainModel(this IServiceCollection services)
        => services.AddAdminAssistantCommonDomainModel();

    public static IServiceCollection AddAdminAssistantServerSideDomainModel(this IServiceCollection services)
    {
        services.AddAdminAssistantCommonDomainModel();

        // Add Infrastructure DomainModel ...
        services.AddTransient<IUserContextProvider, UserContextProvider>();
        return services;
    }

    public static IServiceCollection AddAdminAssistantCommonDomainModel(this IServiceCollection services)
    {
        services.AddAccountsCommonDomainModel();
        services.AddBudgetCommonDomainModel();
        services.AddContactsCommonDomainModel();
        services.AddCoreCommonDomainModel();
        services.AddSharedDomainModel();
        return services;
    }

    private static IServiceCollection AddAccountsCommonDomainModel(this IServiceCollection services)
    {
        services.AddTransient<IBankAccountValidator, BankAccountValidator>();
        services.AddTransient<IBankAccountTransactionValidator, BankAccountTransactionValidator>();
        services.AddTransient<IBankAccountTypeValidator, BankAccountTypeValidator>();
        services.AddTransient<IBankValidator, BankValidator>();
        services.AddTransient<IPayeeValidator, PayeeValidator>();
        return services;
    }

    private static IServiceCollection AddBudgetCommonDomainModel(this IServiceCollection services)
        => services.AddTransient<IBudgetValidator, BudgetValidator>();

    private static IServiceCollection AddContactsCommonDomainModel(this IServiceCollection services)
        => services.AddTransient<IContactValidator, ContactValidator>();

    private static IServiceCollection AddCoreCommonDomainModel(this IServiceCollection services)
        => services.AddTransient<ICurrencyValidator, CurrencyValidator>();

    private static IServiceCollection AddSharedDomainModel(this IServiceCollection services)
        => services;
}
