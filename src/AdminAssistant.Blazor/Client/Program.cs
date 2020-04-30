using Syncfusion.Blazor;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AdminAssistant.Blazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        { 
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQ0NDU4QDMxMzgyZTMxMmUzMGRpK2dWYTRGZExtaEFZN2xrUzRyV3VBYlFZMXJNUVBMNWJHNHhHL1FEcWc9;MjQ0NDU5QDMxMzgyZTMxMmUzMG9TZmpYWkx2WDA4dGI5ZWkzRmNEZVQ4S2lFMkI0d2h4bVFxSTRkRFRXbG89;MjQ0NDYwQDMxMzgyZTMxMmUzMEt6L1hsNzdnVlArZ1pRSXNmYmVPZDJobDR4WUpjelBYUWZLR2xubUJhUTA9;MjQ0NDYxQDMxMzgyZTMxMmUzMGpCY2ZJVUxTK3BsQzVjd3hRNnF5UWxNTjh0SEZwUUNidWM1U0E5VGdiVGM9;MjQ0NDYyQDMxMzgyZTMxMmUzMG4xZ0dkV0k2ZHZkaGc1V2QwbE1qY1IxbnErT3UzS0hIS0QwQmJhOVpOZVk9;MjQ0NDYzQDMxMzgyZTMxMmUzMGpFZzVrOEVZbTMwSUpvWHgyT0puWEtJUXpFTU5oa1NJYUtmcHZrMzRTYzg9;MjQ0NDY0QDMxMzgyZTMxMmUzMEt4c1F6YStzZkhrOHpoQkZOMThPY29zZ1AvdUZhU2NkNndvZ2V3NSs5L009;MjQ0NDY1QDMxMzgyZTMxMmUzMGVFNzdvcjZNUzZidGxWS1RlazY3MHRWSHZ0YnI1VVJxWElPVW9CSzZ6a3c9;MjQ0NDY2QDMxMzgyZTMxMmUzMEhnVjNyWFRmVmcxMFZWTlRmdW9TaXBZQzlzREdzVjNPT2lzQ0hpcjg4ZVk9;NT8mJyc2IWhia31ifWN9Z2FoYmF8YGJ8ampqanNiYmlmamlmanMDHmggOj48PRMwPD4jPCY9Nz88NDowfTA8fSY4;MjQ0NDY3QDMxMzgyZTMxMmUzMENRRnd6bnlnWm1aNk94NmhKNzFKN1JtZll3cEJaeTU0U1ArNkRybzN6VVU9");

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddAdminAssistantClientServices();

            await builder.Build().RunAsync().ConfigureAwait(true);
        }
    }
}
