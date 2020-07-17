using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using FluentValidation;
using AutoMapper;

namespace AdminAssistant.WPF
{
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjg5MjM3QDMxMzgyZTMyMmUzMGRjVW1wckY3MHc5eFB2Qkc4VWRCZnNOYVBrZGZxVGZQOXZ5WFV0LzA2OVk9;Mjg5MjM4QDMxMzgyZTMyMmUzMFF0Vi8vb0ZFcW5KdmdsS2lpMlkxK3I4OE9RVVRBZi9tSlVLc0JrZTFiRlE9;Mjg5MjM5QDMxMzgyZTMyMmUzMFBCQlV4Q2Nwa0RKY0p5dUQwVHRZWkswUEk0ZTNGd3o2RXBrRm1TUGc3TFU9;Mjg5MjQwQDMxMzgyZTMyMmUzMFpoTXc5QmcwZlQySWwzSDJxRVVjcFdHSjBOV05QcE1pMUZCck9XSDM3MWM9;Mjg5MjQxQDMxMzgyZTMyMmUzMFU0QlFtSU5ZMTZrTTNweS9hZEdOcmNmVkFweC9wcUl6cjYxNVhRMWRVVlU9;Mjg5MjQyQDMxMzgyZTMyMmUzMEJvbTNxQ1ZweUtxSXQ3STNnNzZ2cDRDRTlSWTk0S04vaGtKZ00vM0J5cnM9;Mjg5MjQzQDMxMzgyZTMyMmUzMFRZc2hXeU9Ld1FmQXgveXZ2NEl2bzhpcVY1cjFFK1JRUzZDM3Z0Njh2RG89;Mjg5MjQ0QDMxMzgyZTMyMmUzMGllRFdtWHJ2QjIya3liYXUwbGIra2ZTSmdlNG0yZFNEQWtaQXZwT2YrQ3c9;Mjg5MjQ1QDMxMzgyZTMyMmUzMFpXSEdtV1huOXJvLzBUUWpRSWYzcWhFZUcvcVFJdFhHVml3QytqbWkvOHM9;NT8mJyc2IWhia31hfWN9Z2ZoYmF8YGJ8ampqanNiYmlmamlmanMDHmggOj48PRMwPD4jPCY9Nz88NDowfTA8fSY4;Mjg5MjQ2QDMxMzgyZTMyMmUzMEJrOUxhODlxR2Fva3hkWnkweElUYjVld1lTUDlMTEFWaUNiaWxIYVdNdGc9");

            host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    //services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
                    services.AddAutoMapper(typeof(WebAPI.MappingProfile));

                    //services.AddValidatorsFromAssemblyContaining<DomainModel.Modules.Accounts.Validation.BankAccountValidator>();

                    services.AddAdminAssistantDesktopServices();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
#if DEBUG
                    logging.AddConsole();
                    logging.AddDebug();
#else
                    // TODO: Configure production logging.
#endif
                })
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                App.Current.MainWindow = scope.ServiceProvider.GetRequiredService<MainWindow>();
                App.Current.MainWindow.Show();
            }
        }
    }
}
