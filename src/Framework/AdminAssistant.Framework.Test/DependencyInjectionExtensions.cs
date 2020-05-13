using System.Net.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddMocksOfExternalDependencies(this IServiceCollection services)
        {
            var mockLoggerFactory = new Mock<ILoggerFactory>();

            mockLoggerFactory
                .Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(new Mock<ILogger>().Object);

            services.AddTransient((sp) => mockLoggerFactory.Object);
            services.AddTransient(sp => new Mock<HttpClient>().Object);
        }
    }
}

