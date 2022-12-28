using System.Windows;
using AdminAssistant.WPF.Modules.AccountsModule;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Syncfusion.SfSkinManager;

namespace AdminAssistant.WPF;

public partial class App : Application
{
    private readonly IHost host;

    public App()
    {
        // V19.3.0.54
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM2MzcxQDMxMzkyZTMzMmUzMEFQUWZ1UGlGV1JvNUhua2JHdzdTVlVIcVIvUVd0UlE0bmVzK1BGNVhrY1U9; NTM2MzcyQDMxMzkyZTMzMmUzMFQvM2lJSzVnZ2Z2eGI3bmtrSVlueFUyS1NHRFRUVTkrMmxxUmdLeEZEeWc9; NTM2MzczQDMxMzkyZTMzMmUzMEhWWUhYZ09MQUxXYTgySDNpQ3BNMlNCT1lwcEpjanlmai9PR25XMXVOM0k9; NTM2Mzc0QDMxMzkyZTMzMmUzMGd5OVJnaENtREVOWWRwN3Myc0RobjZCVmtjTXcyTGpvNmUzdjVpbXJ2L009; NTM2Mzc1QDMxMzkyZTMzMmUzMFkzWlBicWozR2NjRFNWbFBBTk80YjFyU0VabmdvZWZlNzMzekFHVXczb289; NTM2Mzc2QDMxMzkyZTMzMmUzMGRtVWxmeUtTWTFSM2xEcm42ekpCZmdHVXBYRlRlRllxdkVITTh6YXF4NVE9; NTM2Mzc3QDMxMzkyZTMzMmUzMFJkMDRZck1vSzBoSmdoVkw4TE1HQjFtZFRPU3o4ejJraU5tbWNqNk4vUm89; NTM2Mzc4QDMxMzkyZTMzMmUzMGpKM0xKbVRUR1B2U3dkS2UxZzMzVE5RSEdDRzFDcG4zaEJWYzcxUGRqTjA9; NTM2Mzc5QDMxMzkyZTMzMmUzMGFvQnZPRmlNeWtITlQySktTZkZCUEdVbTBqWEl6OUNackVLcXFtYXkydTA9; NTM2MzgwQDMxMzkyZTMzMmUzMGw5RnB2bVVWano4Nzl0NGVENzg5UEtKYlkxWkZsWUp4Z2lVcEs5S3R2NDA9; NTM2MzgxQDMxMzkyZTMzMmUzME85RnZsOHp5UEFOMjRYL1UwOTRpK2xaQmI4b0NCZmhVZkNnTGFYUytRaXM9");
        // V19.2.0.57 - Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDk0NzEzQDMxMzkyZTMyMmUzME4rWk1peSsxWlVQdXdacjJtQURiSGFoVlYrWHhaWW9qNURRUmpsTW5mZ1k9;NDk0NzE0QDMxMzkyZTMyMmUzMGVRbjZmV1Vhbk8rSEVxQzdTYmJJY0hmYVMxZGhldHFIT0UxTEsxdm1pWnM9;NDk0NzE1QDMxMzkyZTMyMmUzMExIL1ZBOHBmRm40a0hEZmphOWF1VnFoMnF6OVNYaHdTYkNsYzB0MFZ4N289;NDk0NzE2QDMxMzkyZTMyMmUzMGRjV1BXcDdnaDJlTHJEL3lPZXdQaTVQY2swYTdyckpkZmJTNWNBZFR6SDg9;NDk0NzE3QDMxMzkyZTMyMmUzMFJjQ2g0aFdwdVhpbkYvNUZjTjRFejRaQlVMUHEybWNuQXAvalJ6Yk1tT0U9;NDk0NzE4QDMxMzkyZTMyMmUzMFQ3RkNvQXZDUjlnMFVvaU9aTE1IVVpaN2J5LzlKVWFqNGpOS0VSTU5QZUk9;NDk0NzE5QDMxMzkyZTMyMmUzMFlSc01xd3lTN2FBR1l0N0RGTzI2bE9zNEZERXRNS1ljcDBFVkMySXMwM2M9;NDk0NzIwQDMxMzkyZTMyMmUzMFh6OGY0Y3hYTGF1R0FOQVFHN0xyWVVtSzlUY2RKa1IremVBK1NJNmdPYzQ9;NDk0NzIxQDMxMzkyZTMyMmUzMGxxQUg5Wk9nY2lkRVFNMkg4dmVRTTdrdzJXWkRZRVl3QldhcnorVXUrdEU9;NDk0NzIyQDMxMzkyZTMyMmUzMGtBdGFNVjFQQnh0Ynp5a2E4VVRBdFRUTlRqVXZzN0lEY3pXblMzVVcxckE9");
        // V19.1.0.54 - Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDIxOTM1QDMxMzkyZTMxMmUzMGJ6Y3AzVVhlMHlRbUFEUDNnZk1HT2F3VThtZ282MU84QjVab0Q1SG5VQ2c9;NDIxOTM2QDMxMzkyZTMxMmUzMFZ0MXhYZ2RPaGdCME9XRnVHUU5jeWxMMVpFWUJNYVFmMGU5eEZiS0wrMzg9;NDIxOTM3QDMxMzkyZTMxMmUzMFpHWXlCbFRKNVg0M3R4OE1FZUVSOW4vUkxnc2gvUTB1UnFxME9DaGNKWkU9;NDIxOTM4QDMxMzkyZTMxMmUzMEJ5M2JZNTZEbDdjMi9iMXpmT2F5R0J0OTBKVXpHRDRzZEh6ZzhRbmVhWTA9;NDIxOTM5QDMxMzkyZTMxMmUzMGtHSVdTSmsveGcxNHFLOHNkVVZwUy8vMU9kQUNqNzNTcnZ3ZkplQmk1UzQ9;NDIxOTQwQDMxMzkyZTMxMmUzMGFlY29pOXdsMXl1TVBoZlJBWlY1N0ZaZEVPUVhabGJuUCtDcThGQi9ueDg9;NDIxOTQxQDMxMzkyZTMxMmUzMG9rbXQ1UG4yTVpXWW13OTA5eHhOZHZzcE5sdElSYTlDMnJHSi9tbzJheWs9;NDIxOTQyQDMxMzkyZTMxMmUzMFpaMzBtM1FJL3FtcWpaNlFIK05QN2NOMTRIalN1WkhGdkhKZW1Cc2Y3QkE9;NDIxOTQzQDMxMzkyZTMxMmUzMFJiUFRBeHBrL2l1MFp4di9FbUZ2UnBUcE1XWTF1MSt3OWNWc0NPQzV5dVE9;NDIxOTQ0QDMxMzkyZTMxMmUzMEtGcmczQUtXUUUzdTliYXpzUWZReGJoMHY0WGRaUUh2WXlTV1dxT3JQRlU9");
        // V18.4.0.30 - Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzY3NTcxQDMxMzgyZTM0MmUzMEVOQ2lHSkozUytnZ3lqUGZka1QxeXdsZ1BGRlFxSTljcmtQU3N1NlpVc2M9;MzY3NTcyQDMxMzgyZTM0MmUzMGVtK0Z3MWR3Q1FiaW16aWx1R1pWZlRFdkZTYllaWkRCVm1mSGZjSndRZkU9;MzY3NTczQDMxMzgyZTM0MmUzMGF5ZlU0cjczVFZjbGhPTDVHdklvemx2YllEMHN6QUhtNE02Z0xkclZJdms9;MzY3NTc0QDMxMzgyZTM0MmUzMGx2UTJZSFE2SU1XUExUSVl3MFFsUkJZQmdWYnUva1NUdjM2ckZaRFBiSm89;MzY3NTc1QDMxMzgyZTM0MmUzMEpHTEdwKzFKWHZNTFhDZTFEUG5hNHAxc1pYOG9aN1p6UEdvWGNmcWdZNnM9;MzY3NTc2QDMxMzgyZTM0MmUzMEY1ejlNYmYxSEJrRnZOOG5UaUJpdmZJNE5SaGlXRWFKWHdtRG5GZ2Y1azA9;MzY3NTc3QDMxMzgyZTM0MmUzMGYyS0VpakR5QWVTa1o1WjFIdGNKbGQ3VGxWVW9qSGcxNkcycTEwampZTHM9;MzY3NTc4QDMxMzgyZTM0MmUzMG5ZelkwUTQ4WFgwRk9tcDdMNEhpNjBYdkZKcnNNVzd3ZW1aOHVuRTMybVE9;MzY3NTc5QDMxMzgyZTM0MmUzMFppeWcyVTMyZlMyVTFyRHB2YUp5WHEyMW9lWnFVQnFrSDIzY0I2UFFxcHc9;MzY3NTgwQDMxMzgyZTM0MmUzMGU2bU9uV21OcGJkNHZQdisvbjlGckExUUVYVUxtbjlOOXJJOXBjV05pY1E9");

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
            Current.Resources.Add("ViewModelLocator", new ViewModelLocator(scope.ServiceProvider));
            Current.MainWindow = scope.ServiceProvider.GetRequiredService<MainWindow>();
            Current.MainWindow.Show();
        }
    }

    private void Application_Startup(object sender, StartupEventArgs e) => SfSkinManager.ApplyStylesOnApplication = true;
}
