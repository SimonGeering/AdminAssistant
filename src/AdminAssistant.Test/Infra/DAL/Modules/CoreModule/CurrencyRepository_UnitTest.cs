#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.EntityFramework.Model.Core;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Modules.CoreModule;
using AdminAssistant.Modules.CoreModule.Infrastructure.DAL;
using AdminAssistant.Shared;
using MappingProfile = AdminAssistant.Infrastructure.MappingProfile;

namespace AdminAssistant.Test.Infra.DAL.Modules.CoreModule;

public sealed class CurrencyRepository_GetListAsync
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_PopulatedCurrencyList_WhenDatabaseHasData()
    {
        // Arrange
        var mapper = new ServiceCollection().AddAutoMapper(typeof(MappingProfile)).BuildServiceProvider().GetRequiredService<IMapper>();
        var currencyList = new List<Currency>()
            {
                Factory.Currency.WithTestData(10).Build(),
                Factory.Currency.WithTestData(20).Build()
            };
        var currencyData = mapper.Map<IList<CurrencyEntity>>(currencyList);

        var mockDbContext = new Mock<ApplicationDbContext>();
        mockDbContext.Setup(x => x.Currencies)
            .Returns(currencyData.AsQueryable().BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient((sp) => new Mock<IDateTimeProvider>().Object);
        services.AddTransient((sp) => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra();
        services.AddTransient((sp) => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<ICurrencyRepository>().GetListAsync(default);

        // Assert
        result.Should().HaveCount(currencyList.Count);
        result.Should().BeEquivalentTo(currencyList);
    }
}

public class CurrencyRepository_GetAsync
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_ACurrency_WhenDatabaseContainsAnItemWithTheGivenID()
    {
        // Arrange
        var mapper = new ServiceCollection().AddAutoMapper(typeof(MappingProfile)).BuildServiceProvider().GetRequiredService<IMapper>();

        var currencyList = new List<Currency>()
            {
                Factory.Currency.WithTestData(10).Build(),
                Factory.Currency.WithTestData(20).Build()
            };
        var currencyData = mapper.Map<IList<CurrencyEntity>>(currencyList);

        var mockDbContext = new Mock<ApplicationDbContext>();
        mockDbContext.Setup(x => x.Currencies)
            .Returns(currencyData.AsQueryable().BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient((sp) => new Mock<IDateTimeProvider>().Object);
        services.AddTransient((sp) => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra();
        services.AddTransient((sp) => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<ICurrencyRepository>().GetAsync(currencyList[Constants.FirstItem].CurrencyID, default);

        // Assert
        result.Should().BeEquivalentTo(currencyList[Constants.FirstItem]);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
