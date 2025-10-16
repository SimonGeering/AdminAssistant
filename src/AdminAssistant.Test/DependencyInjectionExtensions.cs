using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void AddMockDbContext(this IServiceCollection services, Mock<ApplicationDbContext> mockDbContext)
        => services.AddTransient(_ => mockDbContext.Object);

    public static void AddMockDbContext(this IServiceCollection services)
        => services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestDb"));

    public static void AddMockUserContextProvider(this IServiceCollection services)
    {
        var result = new User { SignOn = "TestUser" };
        services.AddMockUserContextProvider(result);
    }

    private static void AddMockUserContextProvider(this IServiceCollection services, User user)
    {
        var mockUserContext = new Mock<IUserContextProvider>();
        mockUserContext.Setup(x => x.GetCurrentUser()).Returns(user);
        services.AddTransient(_ => mockUserContext.Object);
    }

    public static void AddMockDateTimeProvider(this IServiceCollection services)
        => services.AddMockDateTimeProvider(DateTime.UtcNow);

    public static void AddMockDateTimeProvider(this IServiceCollection services, DateTime dateTime)
    {
        var mockUserContext = new Mock<IDateTimeProvider>();
        mockUserContext.Setup(x => x.UtcNow).Returns(dateTime);
        services.AddTransient(_ => mockUserContext.Object);
    }

    public static void AddMocksOfExternalServerSideDependencies(this IServiceCollection services)
    {
        services.AddMockDbContext();
        services.AddMockServerSideLogging();
        services.AddTransient(_ => new Mock<IMapper>().Object);
    }

    public static void AddMocksOfExternalClientSideDependencies(this IServiceCollection services)
    {
        services.AddMockClientSideLogging();
        services.AddTransient(_ => new Mock<IMapper>().Object);
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
        mockLogger.Setup(m => m.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception?>(), It.IsAny<Func<object, Exception?, string>>()))
            .Callback<LogLevel, EventId, object, Exception?, Func<object, Exception?, string>>((logLevel, eventId, state, exception, formatter) => Console.WriteLine($"{state}"));

        var mockLoggerFactory = new Mock<ILoggerFactory>();
        mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>()))
            .Returns(() => mockLogger.Object);

        services.AddTransient((sp) => mockLoggerFactory.Object);
        // TODO: look at if this can be refactored to allow
        services.AddTransient<ILoggingProvider, ServerSideLoggingProvider>();
    }
}
