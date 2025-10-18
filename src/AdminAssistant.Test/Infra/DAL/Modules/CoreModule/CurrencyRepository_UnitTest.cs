// ReSharper disable InconsistentNaming
#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.Domain;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Modules.CoreModule;
using AdminAssistant.Modules.CoreModule.Infrastructure.DAL;
using AdminAssistant.Shared;

namespace AdminAssistant.Test.Infra.DAL.Modules.CoreModule;

public sealed class CurrencyRepository_GetListAsync
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_PopulatedCurrencyList_WhenDatabaseHasData()
    {
        // Arrange
        var currencyList = new List<Currency>()
            {
                Factory.Currency.WithTestData(10).Build(),
                Factory.Currency.WithTestData(20).Build()
            };

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.Currencies)
            .Returns(currencyList.ToCurrencyEntityList().BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddTransient(_ => new Mock<IDateTimeProvider>().Object);
        services.AddTransient(_ => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra();
        services.AddTransient(_ => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<ICurrencyRepository>().GetListAsync(CancellationToken.None);

        // Assert
        result.Count.ShouldBe(currencyList.Count);
        result.ShouldBeEquivalentTo(currencyList);
    }
}

public class CurrencyRepository_GetAsync
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_ACurrency_WhenDatabaseContainsAnItemWithTheGivenID()
    {
        // Arrange
        var currencyList = new List<Currency>()
            {
                Factory.Currency.WithTestData(10).Build(),
                Factory.Currency.WithTestData(20).Build()
            };

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.Currencies)
            .Returns(currencyList.ToCurrencyEntityList().BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddTransient(_ => new Mock<IDateTimeProvider>().Object);
        services.AddTransient(_ => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra();
        services.AddTransient(_ => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<ICurrencyRepository>().GetAsync(currencyList[Constants.FirstItem].CurrencyID, default);

        // Assert
        result.ShouldBeEquivalentTo(currencyList[Constants.FirstItem]);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
