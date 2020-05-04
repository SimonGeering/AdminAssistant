using System.Net.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddMocksOfExternalDependencies(this IServiceCollection services)
        {
            services.AddTransient(sp => new Mock<ILoggerFactory>().Object);
            services.AddTransient(sp => new Mock<HttpClient>().Object);
        }
    }
}

