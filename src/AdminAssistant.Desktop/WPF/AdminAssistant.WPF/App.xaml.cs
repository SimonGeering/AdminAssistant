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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzU1ODIzQDMxMzgyZTMzMmUzMEFveEs3YUcrWmdjUnJRQi80Vzd0WUZQalVRcjA3T09RMHB3ZW55dHcxRnM9;MzU1ODI0QDMxMzgyZTMzMmUzME04V1UxYWo3WW0rQ2VYMmQrTXpqemZydjQvdTVkZlUvMnBBR2cxM1F0cEk9;MzU1ODI1QDMxMzgyZTMzMmUzMG5WeFBVZHJua1hZZFhid05NZWliMnJRTHVHbkJjSENTMS9odWkyeGR6VXM9;MzU1ODI2QDMxMzgyZTMzMmUzMEZ6cDFJUnFPZENId2FqUE92cERNemN1Z1RhWmRqcWxvUGZoeS93ZHZKWWc9;MzU1ODI3QDMxMzgyZTMzMmUzMEpjbjQ3anZlRU11aDczNkNCSWF6OVhlVkJFaVJ1TjVWNnBRQXVsYzEwazQ9;MzU1ODI4QDMxMzgyZTMzMmUzMEJwWXVaSmszTFFYMFpNUnA0MDdEem5aMU5QdFhoaEJXQVd2b0FsVE4vZGM9;MzU1ODI5QDMxMzgyZTMzMmUzMFMvYnErUnFtb2dDWEpucG92R1dGU2RDaVA2TEhhdkNkR1lzMThyLzVIbm89;MzU1ODMwQDMxMzgyZTMzMmUzMFZ6cTRTZTh1cVZYcGNFZWNaZE9yaHhNRndldUt1THljZFJlejlLRkVva1k9;MzU1ODMxQDMxMzgyZTMzMmUzMEc1OUNiNHJhUVNVMUkzS0xNTWpINjdjNFdQbHRkZTBXNVYzblJzVFRwTG89;MzU1ODMyQDMxMzgyZTMzMmUzME14YkN4YU1PYkVoU1Z2QVFoRVZ1bEZwVkJuOXkwMnBQOW11azRud3BDaEE9");

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
