using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FluentValidation;
using Syncfusion.SfSkinManager;
using AdminAssistant.WPF.Modules.AccountsModule;
using System;

namespace AdminAssistant.WPF
{
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzY3NTcxQDMxMzgyZTM0MmUzMEVOQ2lHSkozUytnZ3lqUGZka1QxeXdsZ1BGRlFxSTljcmtQU3N1NlpVc2M9;MzY3NTcyQDMxMzgyZTM0MmUzMGVtK0Z3MWR3Q1FiaW16aWx1R1pWZlRFdkZTYllaWkRCVm1mSGZjSndRZkU9;MzY3NTczQDMxMzgyZTM0MmUzMGF5ZlU0cjczVFZjbGhPTDVHdklvemx2YllEMHN6QUhtNE02Z0xkclZJdms9;MzY3NTc0QDMxMzgyZTM0MmUzMGx2UTJZSFE2SU1XUExUSVl3MFFsUkJZQmdWYnUva1NUdjM2ckZaRFBiSm89;MzY3NTc1QDMxMzgyZTM0MmUzMEpHTEdwKzFKWHZNTFhDZTFEUG5hNHAxc1pYOG9aN1p6UEdvWGNmcWdZNnM9;MzY3NTc2QDMxMzgyZTM0MmUzMEY1ejlNYmYxSEJrRnZOOG5UaUJpdmZJNE5SaGlXRWFKWHdtRG5GZ2Y1azA9;MzY3NTc3QDMxMzgyZTM0MmUzMGYyS0VpakR5QWVTa1o1WjFIdGNKbGQ3VGxWVW9qSGcxNkcycTEwampZTHM9;MzY3NTc4QDMxMzgyZTM0MmUzMG5ZelkwUTQ4WFgwRk9tcDdMNEhpNjBYdkZKcnNNVzd3ZW1aOHVuRTMybVE9;MzY3NTc5QDMxMzgyZTM0MmUzMFppeWcyVTMyZlMyVTFyRHB2YUp5WHEyMW9lWnFVQnFrSDIzY0I2UFFxcHc9;MzY3NTgwQDMxMzgyZTM0MmUzMGU2bU9uV21OcGJkNHZQdisvbjlGckExUUVYVUxtbjlOOXJJOXBjV05pY1E9");

            host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddAdminAssistantWebAPIClient(new Uri("https://localhost:5001"));

                    services.AddValidatorsFromAssemblyContaining<Infra.DAL.IDatabasePersistable>();

                    services.AddAdminAssistantClientSideProviders();
                    services.AddAdminAssistantClientSideDomainModel();
                    services.AddAdminAssistantUI();

                    services.AddSingleton<MainWindow>();
                    services.AddTransient<BankAccountEditDialog>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
#if DEBUG
                    logging.AddConsole();
                    logging.AddDebug();

                    logging.AddFilter("Default", LogLevel.Information)
                           .AddFilter(Infra.Providers.ILoggingProvider.ClientSideLogCategory, LogLevel.Debug)
                           .AddFilter("Microsoft", LogLevel.Warning)
                           .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);
#else
                    logging.AddFilter("Default", LogLevel.Warning)
                           .AddFilter(Infra.Providers.ILoggingProvider.ClientSideLogCategory, LogLevel.Warning)
                           .AddFilter("Microsoft", LogLevel.Warning)
                           .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);

                    // TODO: Configure production logging.
#endif
                })
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                App.Current.Resources.Add("ViewModelLocator", new ViewModelLocator(scope.ServiceProvider));
                App.Current.MainWindow = scope.ServiceProvider.GetRequiredService<MainWindow>();
                App.Current.MainWindow.Show();
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e) => SfSkinManager.ApplyStylesOnApplication = true;
    }
}
