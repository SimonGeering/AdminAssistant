using Moq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddMocksOfExternalDependencies(this IServiceCollection services)
        {
            services.AddTransient(sp => new Mock<System.Net.Http.HttpClient>().Object);
        }
    }
}

