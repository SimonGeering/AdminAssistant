#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using Ardalis.GuardClauses;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AdminAssistant
{
    [Collection("SequentialDBBackedTests")]
    public abstract class IntegrationTestBase : IDisposable
    {
        private readonly IHost _testServer;
        private readonly Respawn.Checkpoint _checkpoint;
        private readonly string _connectionString;
        private readonly HttpClient _httpClient;

        public IntegrationTestBase()
        {
            var hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
#if DEBUG
                    logging.AddConsole();
                    logging.AddDebug();

                    logging.AddFilter("Default", LogLevel.Information)
                            .AddFilter(Framework.Providers.ILoggingProvider.ServerSideLogCategory, LogLevel.Debug)
                            .AddFilter("Microsoft", LogLevel.Warning)
                            .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information);
#else
                    logging.AddFilter("Default", LogLevel.Warning)
                           .AddFilter(Framework.Providers.ILoggingProvider.ServerSideLogCategory, LogLevel.Warning)
                           .AddFilter("Microsoft", LogLevel.Warning)
                           .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Warning);

                    // TODO: Configure production logging.
#endif
                })
                .ConfigureAppConfiguration((hostingContext, config) => config.AddUserSecrets(Assembly.GetExecutingAssembly()))
                .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Blazor.Server.Startup>();
                     webBuilder.ConfigureTestServices(ConfigureTestServices());
                     webBuilder.UseTestServer();
                 });

            _testServer = hostBuilder.Start();
            _connectionString = _testServer.Services.GetRequiredService<IApplicationDbContext>().ConnectionString.Replace("Application Name=AdminAssistant;", "Application Name=AdminAssistant_TestDBReset", StringComparison.InvariantCulture);

            _checkpoint = new Respawn.Checkpoint
            {
                TablesToIgnore = new[]
                {
                    "sysdiagrams",
                    "__EFMigrationsHistory",
                    "tblObjectType",
                    "Currency",
                    "BankAccountType"
                },
                WithReseed = true
            };

            Container = _testServer.Services;
            _httpClient = _testServer.GetTestClient();
        }

        protected IServiceProvider Container { get; }

        protected async Task ResetDatabaseAsync() => await _checkpoint.Reset(_connectionString).ConfigureAwait(false);

        protected virtual Action<IServiceCollection> ConfigureTestServices() => services =>
        {
            // Register the WebAPIClient using the test httpClient ... 
            services.AddTransient<IAdminAssistantWebAPIClient>((sp) =>
            {
                Guard.Against.Null(_httpClient.BaseAddress, "httpClient.BaseAddress");
                return new AdminAssistantWebAPIClient(_httpClient) { BaseUrl = _httpClient.BaseAddress.ToString() };
            });
            services.AddAutoMapper(typeof(MappingProfile));
        };

#region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Clean up DB if tests failed ...
                    ResetDatabaseAsync().ConfigureAwait(false);

                    // dispose managed state (managed objects)
                    _testServer.Dispose();
                    _httpClient.Dispose();
                }

                // free unmanaged resources (unmanaged objects) and override finalizer
                // set large fields to null
                disposedValue = true;
            }
        }

        // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        //~IntegrationTestBase()
        //{
        //    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //    Dispose(disposing: false);
        //}

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
#endregion // IDisposable
    }
}
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
