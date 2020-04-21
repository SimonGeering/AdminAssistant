using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdminAssistant.Blazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddDevExpressBlazor(); // Do this before ConfigureServices to prevent complications in DI unit testing.

            builder.Services.AddAdminAssistantClientServices();

            await builder.Build().RunAsync().ConfigureAwait(true);
        }
    }
}
