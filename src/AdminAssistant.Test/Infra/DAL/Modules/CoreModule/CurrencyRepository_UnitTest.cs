#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Core;
using AdminAssistant.Infra.Providers;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace AdminAssistant.Infra.DAL.Modules.CoreModule
{
    public class CurrencyRepository_GetListAsync
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

            var mockDbContext = new Mock<IApplicationDbContext>();
            mockDbContext.Setup(x => x.Currencies)
                .Returns(currencyData.AsQueryable().BuildMockDbSet().Object);

            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient((sp) => new Mock<IDateTimeProvider>().Object);
            services.AddTransient((sp) => new Mock<IUserContextProvider>().Object);
            services.AddAdminAssistantServerSideInfra(new ConfigurationSettings() { ConnectionString = "FakeConnectionString", DatabaseProvider = "SQLServerLocalDB" });
            services.AddTransient((sp) => mockDbContext.Object);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<ICurrencyRepository>().GetListAsync().ConfigureAwait(false);

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

            var mockDbContext = new Mock<IApplicationDbContext>();
            mockDbContext.Setup(x => x.Currencies)
                .Returns(currencyData.AsQueryable().BuildMockDbSet().Object);

            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient((sp) => new Mock<IDateTimeProvider>().Object);
            services.AddTransient((sp) => new Mock<IUserContextProvider>().Object);
            services.AddAdminAssistantServerSideInfra(new ConfigurationSettings() { ConnectionString = "FakeConnectionString", DatabaseProvider = "SQLServerLocalDB" });
            services.AddTransient((sp) => mockDbContext.Object);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<ICurrencyRepository>().GetAsync(currencyList.First().CurrencyID).ConfigureAwait(false);

            // Assert
            result.Should().BeEquivalentTo(currencyList.First());
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
