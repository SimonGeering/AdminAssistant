using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdminAssistant
{
    public abstract class IntegrationTestBase : IDisposable
    {
        private readonly IHost testServer = null!;

        protected HttpClient HttpClient { get; }

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
            this.HttpClient = this.testServer.GetTestClient();
        }

        protected abstract Action<IServiceCollection> ConfigureTestServices();

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
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
