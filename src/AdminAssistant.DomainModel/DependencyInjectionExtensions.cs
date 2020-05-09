using AdminAssistant.DomainModel.Infrastructure;
using AdminAssistant.DomainModel.Modules.Accounts.CQRS;
using AdminAssistant.DomainModel.Modules.Accounts.Validation;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddAdminAssistantClientSideDomainModel(this IServiceCollection services)
        {
            AddSharedDomainModel(services);
        }
        public static void AddAdminAssistantServerSideDomainModel(this IServiceCollection services)
        {
            AddSharedDomainModel(services);

            // AddInfrastructureDomainModel ...
            services.AddTransient<IConfigurationManager, ConfigurationManager>();
            services.AddTransient<IUserContextProvider, UserContextProvider>();

            // Set-up Add MediatR ...
            services.AddMediatR(typeof(GetBankAccountByIDHandler));
        }

        private static void AddSharedDomainModel(IServiceCollection services)
        {
            // AddAccountsDomainModel ...
            services.AddTransient<IBankAccountValidator, BankAccountValidator>();
            services.AddTransient<ICurrencyValidator, CurrencyValidator>();
        }
    }
}

