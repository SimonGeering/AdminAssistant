using AdminAssistant.DomainModel.Modules.Accounts.Validation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AdminAssistant.DomainModel
{
    public static class ServicesModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBankAccountValidator, BankAccountValidator>();
            services.AddTransient<ICurrencyValidator, CurrencyValidator>();

            services.AddMediatR(typeof(ServicesModule));
        }
    }
}
