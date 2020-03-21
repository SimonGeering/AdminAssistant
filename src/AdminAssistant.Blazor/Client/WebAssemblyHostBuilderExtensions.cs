using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdminAssistant.Blazor.Client
{
    public static class WebAssemblyHostBuilderExtensions
    {
        public static void ConfigureServices(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddAdminAssistantClientSideDomainModel();
            builder.Services.AddAdminAssistantUI();
        }
    }
}
