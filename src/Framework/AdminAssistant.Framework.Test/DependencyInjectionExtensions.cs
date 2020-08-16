using System;
using AdminAssistant.Framework.Providers;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddMocksOfExternalServerSideDependencies(this IServiceCollection services)
        {
            services.AddMockServerSideLogging();
            services.AddTransient((sp) => new Mock<IMapper>().Object);
        }

        public static void AddMocksOfExternalClientSideDependencies(this IServiceCollection services)
        {
            services.AddMockClientSideLogging();
            services.AddTransient((sp) => new Mock<IMapper>().Object);
        }

        public static void AddMockServerSideLogging(this IServiceCollection services)
        {
            AddMockLogging(services);    

            services.AddTransient<ILoggingProvider, ServerSideLoggingProvider>();
        }

        public static void AddMockClientSideLogging(this IServiceCollection services)
        {
            AddMockLogging(services);
            services.AddTransient<ILoggingProvider, ClientSideLoggingProvider>();
        }

        private static void AddMockLogging(IServiceCollection services)
        {
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(m => m.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback<LogLevel, EventId, object, Exception, Func<object, Exception, string>>((logLevel, eventId, state, exception, formatter) => Console.WriteLine($"{state}"));

            var mockLoggerFactory = new Mock<ILoggerFactory>();
            mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(() => mockLogger.Object);

            services.AddTransient((sp) => mockLoggerFactory.Object);
            // TODO: look at if this can be refactored to allow 
            services.AddTransient<ILoggingProvider, ServerSideLoggingProvider>();
        }
    }
}

