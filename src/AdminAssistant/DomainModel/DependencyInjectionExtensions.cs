using AdminAssistant.DomainModel.Shared;
using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using AdminAssistant.DomainModel.Modules.AccountsModule.Validation;
using AdminAssistant.DomainModel.Modules.BudgetModule.Validation;
using MediatR;
using AdminAssistant.DomainModel.Modules.CoreModule.Validation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class DependencyInjectionExtensions
    {
        public static void AddAdminAssistantClientSideDomainModel(this IServiceCollection services)
        {
            AddAccountsDomainModel(services);
            AddBudgetDomainModel(services);
            AddCoreDomainModel(services);
        }

        public static void AddAdminAssistantServerSideDomainModel(this IServiceCollection services)
        {
            AddAdminAssistantClientSideDomainModel(services);

            // AddInfrastructureDomainModel ...
            services.AddTransient<IUserContextProvider, UserContextProvider>();

            // Set-up Add MediatR ...
            services.AddMediatR(typeof(BankAccountByIDQuery));
        }

        private static void AddAccountsDomainModel(IServiceCollection services)
        {
            services.AddTransient<IBankAccountValidator, BankAccountValidator>();
            services.AddTransient<IBankAccountTransactionValidator, BankAccountTransactionValidator>();
            services.AddTransient<IBankAccountTypeValidator, BankAccountTypeValidator>();            
            services.AddTransient<IBankValidator, BankValidator>();
        }

        private static void AddBudgetDomainModel(IServiceCollection services)
            => services.AddTransient<IBudgetValidator, BudgetValidator>();

        private static void AddCoreDomainModel(IServiceCollection services)
            => services.AddTransient<ICurrencyValidator, CurrencyValidator>();
    }
}

