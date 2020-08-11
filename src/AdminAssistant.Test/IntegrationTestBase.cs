using System;
using System.Net.Http;
using System.Threading.Tasks;
using AdminAssistant.DAL.EntityFramework;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace AdminAssistant
{
    [Collection("SequentialDBBackedTests")]
    public abstract class IntegrationTestBase : IDisposable
    {
        private readonly IHost testServer;
        private readonly Respawn.Checkpoint checkpoint;
        private readonly string connectionString;
        private readonly HttpClient httpClient;

        public IntegrationTestBase()
        {
            var hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Blazor.Server.Startup>();
                    webBuilder.ConfigureTestServices(ConfigureTestServices());
                    webBuilder.UseTestServer();
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddUserSecrets(System.Reflection.Assembly.GetExecutingAssembly());
                });

            this.testServer = hostBuilder.Start();
            this.connectionString = this.testServer.Services.GetService<IApplicationDbContext>().ConnectionString.Replace("Application Name=AdminAssistant;", "Application Name=AdminAssistant_TestDBReset", StringComparison.InvariantCulture);

            this.checkpoint = new Respawn.Checkpoint
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

            this.Container = this.testServer.Services;
            this.httpClient = this.testServer.GetTestClient();
        }

        protected IServiceProvider Container { get; }

        protected async Task ResetDatabaseAsync() => await this.checkpoint.Reset(this.connectionString).ConfigureAwait(false);

        protected virtual Action<IServiceCollection> ConfigureTestServices() => services =>
        {
            // Register the WebAPIClient using the test httpClient ... 
            services.AddTransient<IAdminAssistantWebAPIClient>((sp) => new AdminAssistantWebAPIClient(this.httpClient) { BaseUrl = this.httpClient.BaseAddress.ToString() } );
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
                    this.ResetDatabaseAsync().ConfigureAwait(false);

                    // dispose managed state (managed objects)
                    this.testServer.Dispose();
                    this.httpClient.Dispose();
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
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion // IDisposable
    }
}
