using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.DomainModel.Modules.BudgetModule.Validation;
using AdminAssistant.DomainModel.Modules.ContactsModule.CQRS;
using AdminAssistant.DomainModel.Modules.CoreModule.Validation;
using AdminAssistant.DomainModel.Shared;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjectionExtensions
{
    public static void AddAdminAssistantClientSideDomainModel(this IServiceCollection services)
        => AddAdminAssistantCommonDomainModel(services);

    public static void AddAdminAssistantServerSideDomainModel(this IServiceCollection services)
    {
        AddAdminAssistantCommonDomainModel(services);

        // AddInfrastructureDomainModel ...
        services.AddTransient<IUserContextProvider, UserContextProvider>();

        // Set-up Add MediatR ...
        services.AddMediatR(typeof(BankAccountByIDQuery), typeof(BankAccountByIDQueryHandler));
        services.AddMediatR(typeof(ContactByIDQuery), typeof(ContactByIDQueryHandler));
    }

    public static void AddAdminAssistantCommonDomainModel(this IServiceCollection services)
    {
        AddAccountsCommonDomainModel(services);
        AddBudgetCommonDomainModel(services);
        AddCoreCommonDomainModel(services);
    }

    private static void AddAccountsCommonDomainModel(IServiceCollection services)
    {
        services.AddTransient<IBankAccountValidator, BankAccountValidator>();
        services.AddTransient<IBankAccountTransactionValidator, BankAccountTransactionValidator>();
        services.AddTransient<IBankAccountTypeValidator, BankAccountTypeValidator>();
        services.AddTransient<IBankValidator, BankValidator>();
        services.AddTransient<IPayeeValidator, PayeeValidator>();
    }

    private static void AddBudgetCommonDomainModel(IServiceCollection services)
        => services.AddTransient<IBudgetValidator, BudgetValidator>();

    private static void AddCoreCommonDomainModel(IServiceCollection services)
        => services.AddTransient<ICurrencyValidator, CurrencyValidator>();
}
