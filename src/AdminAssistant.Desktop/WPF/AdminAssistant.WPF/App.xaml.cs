using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FluentValidation;
using AutoMapper;

namespace AdminAssistant.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjczNDkwQDMxMzgyZTMxMmUzMEFoQys4dEsraHhVR1NsUDkreFpySS8weHNEZlZNdGdMNUllSnkwVGtCckk9;MjczNDkxQDMxMzgyZTMxMmUzMGpBYzByQXJ1Zzc5Y1FjU3E5WGFTOHdOaUdKdXlSYXdFQzU0cGk5UVpNNTA9;MjczNDkyQDMxMzgyZTMxMmUzME9hZStqN1oxakhtSVU0WStKM1NqWVg1RFgzVFIramhmMXJwM1JrSUtaamc9;MjczNDkzQDMxMzgyZTMxMmUzMGNDczZQZkN5YWt2RWs2TktjVU1OQVg4R3V0bkx3VFl1UC96UGo4SEdIMTQ9;MjczNDk0QDMxMzgyZTMxMmUzMGd1M0hoWnBuUm9yQXJTdzU5T1pDSjVRTUNENHFocE50Zml4ZVM4QkVoM2M9;MjczNDk1QDMxMzgyZTMxMmUzMG93NklEa2hKSm1TL3BDSUpMT01uTHIrTS9jUUxRWDZ0ZC9ZZzgxYzc1Y2s9;MjczNDk2QDMxMzgyZTMxMmUzMFZheE10ZVc4VjZ5NGNyV3g0WEpsOEdKYVQ5VlRmd1hOS2M2cys3bUJLbk09;MjczNDk3QDMxMzgyZTMxMmUzMEVzR2ZmRlRudlhUNGRMMDhqSVV3RFovQVJ2OGRiWTlqcHcveURyOGo5cUU9;MjczNDk4QDMxMzgyZTMxMmUzMEdoRnJCbVNOenR0ZHhoczI0WVNybUFDUDZWWGZ1dDJ3S3lwNU1rVWxGNk09;NT8mJyc2IWhia31ifWN9ZmFoYmF8YGJ8ampqanNiYmlmamlmanMDHmggOj48PRMwPD4jPCY9Nz88NDowfTA8fSY4;MjczNDk5QDMxMzgyZTMxMmUzMGg0dm40MFpuWlpCSHVlWHR1R2IvcWlNR3J2R1F3ZFZKa1lWMytzb2pKQjA9");

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
