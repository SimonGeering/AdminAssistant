using System;
using System.Net.Http;
using System.Threading.Tasks;
using AdminAssistant.DAL.EntityFramework;
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

            this.DbContext = this.testServer.Services.GetService<IApplicationDbContext>();

            this.HttpClient = this.testServer.GetTestClient();

            this.checkpoint = new Respawn.Checkpoint {
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
        }

        protected IApplicationDbContext DbContext { get; }
        protected HttpClient HttpClient { get; }

        protected async Task ResetDatabaseAsync()
        {
            var connectionStirng = this.DbContext.ConnectionString.Replace("Application Name=AdminAssistant;", "Application Name=AdminAssistant_TestDBReset", StringComparison.InvariantCulture);
            await this.checkpoint.Reset(connectionStirng).ConfigureAwait(false);
        }

        protected virtual Action<IServiceCollection> ConfigureTestServices() => services => { };

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
                    this.DbContext.Dispose();
                    this.testServer.Dispose();
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
    }
}
