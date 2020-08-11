using System;
using AdminAssistant.Framework.Providers;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void AddMocksOfExternalDependencies(this IServiceCollection services)
        {
            services.AddMockLogging();
            services.AddTransient((sp) => new Mock<IMapper>().Object);
        }

        public static void AddMockLogging(this IServiceCollection services)
        {
            var mockLogger = new Mock<ILogger>();
            mockLogger.Setup(m => m.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback<LogLevel, EventId, object, Exception, Func<object, Exception, string>>((logLevel, eventId, state, exception, formatter) => Console.WriteLine($"{state}"));

            var mockLoggerFactory = new Mock<ILoggerFactory>();
            mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>()))
                .Returns(() => mockLogger.Object);

            services.AddTransient((sp) => mockLoggerFactory.Object);
            // TODO: look at if this can be refactored to allow 
            services.AddTransient<ILoggingProvider, LoggingProvider>();
        }
    }
}

