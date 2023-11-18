#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;

namespace AdminAssistant.Test.Infra.DAL.Modules.AccountsModule;

public sealed class BankAccountTypeRepository_GetListAsync
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_PopulatedBankAccountTypeList_WhenDatabaseHasData()
    {
        // Arrange
        var mapper = new ServiceCollection().AddAutoMapper(typeof(MappingProfile)).BuildServiceProvider().GetRequiredService<IMapper>();
        var bankAccountTypeList = new List<BankAccountType>()
            {
                Factory.BankAccountType.WithTestData(10).Build(),
                Factory.BankAccountType.WithTestData(20).Build()
            };
        var data = mapper.Map<IList<BankAccountTypeEntity>>(bankAccountTypeList);

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.BankAccountTypes)
            .Returns(data.AsQueryable().BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient((sp) => new Mock<IDateTimeProvider>().Object);
        services.AddTransient((sp) => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra(new ConfigurationSettings() { ConnectionString = "FakeConnectionString", DatabaseProvider = "SQLServerLocalDB" });
        services.AddTransient((sp) => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountTypeRepository>().GetListAsync().ConfigureAwait(false);

        // Assert
        result.Should().HaveCount(bankAccountTypeList.Count);
        result.Should().BeEquivalentTo(bankAccountTypeList);
    }
}

public class BankAccountTypeRepository_GetAsync
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_ABankAccountType_WhenDatabaseContainsAnItemWithTheGivenID()
    {
        // Arrange
        var mapper = new ServiceCollection().AddAutoMapper(typeof(MappingProfile)).BuildServiceProvider().GetRequiredService<IMapper>();

        var bankAccountTypeList = new List<BankAccountType>()
            {
                Factory.BankAccountType.WithTestData(10).Build(),
                Factory.BankAccountType.WithTestData(20).Build()
            };
        var data = mapper.Map<IList<BankAccountTypeEntity>>(bankAccountTypeList);

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.BankAccountTypes)
            .Returns(data.AsQueryable().BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient((sp) => new Mock<IDateTimeProvider>().Object);
        services.AddTransient((sp) => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra(new ConfigurationSettings() { ConnectionString = "FakeConnectionString", DatabaseProvider = "SQLServerLocalDB" });
        services.AddTransient((sp) => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountTypeRepository>().GetAsync(bankAccountTypeList[Constants.FirstItem].BankAccountTypeID).ConfigureAwait(false);

        // Assert
        result.Should().BeEquivalentTo(bankAccountTypeList[Constants.FirstItem]);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
