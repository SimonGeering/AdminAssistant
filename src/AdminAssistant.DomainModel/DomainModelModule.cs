using AdminAssistant.Accounts.DomainModel.Validation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AdminAssistant.Accounts.DomainModel
{
    public static class ServicesModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IBankAccountService, BankAccountService>();
            //services.AddTransient<IBankAccountTypeService, BankAccountTypeService>();
            //services.AddTransient<ICurrencyService, CurrencyService>();
            //
            services.AddTransient<IBankAccountValidator, BankAccountValidator>();
            services.AddTransient<ICurrencyValidator, CurrencyValidator>();

            services.AddMediatR(typeof(ServicesModule));
        }
    }
}
