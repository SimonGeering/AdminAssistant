// ReSharper disable InconsistentNaming
#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.Domain;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;
using AdminAssistant.Shared;

namespace AdminAssistant.Test.Infra.DAL.Modules.AccountsModule;

public sealed class BankAccountTypeRepository_GetListAsync
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_PopulatedBankAccountTypeList_WhenDatabaseHasData()
    {
        // Arrange
        var bankAccountTypeList = new List<BankAccountType>()
            {
                Factory.BankAccountType.WithTestData(10).Build(),
                Factory.BankAccountType.WithTestData(20).Build()
            };
        var data = bankAccountTypeList.ToBankAccountTypeEntityList();

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.BankAccountTypes)
            .Returns(data.BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddTransient(_ => new Mock<IDateTimeProvider>().Object);
        services.AddTransient(_ => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra();
        services.AddTransient(_ => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountTypeRepository>().GetListAsync(CancellationToken.None);

        // Assert
        result.Count.ShouldBe(bankAccountTypeList.Count);
        result.ShouldBeEquivalentTo(bankAccountTypeList);
    }
}

public class BankAccountTypeRepository_GetAsync
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_ABankAccountType_WhenDatabaseContainsAnItemWithTheGivenID()
    {
        // Arrange
        var bankAccountTypeList = new List<BankAccountType>()
            {
                Factory.BankAccountType.WithTestData(10).Build(),
                Factory.BankAccountType.WithTestData(20).Build()
            };

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.BankAccountTypes)
            .Returns(bankAccountTypeList.ToBankAccountTypeEntityList().BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddTransient(_ => new Mock<IDateTimeProvider>().Object);
        services.AddTransient(_ => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra();
        services.AddTransient(_ => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountTypeRepository>().GetAsync(bankAccountTypeList[Constants.FirstItem].BankAccountTypeID, CancellationToken.None);

        // Assert
        result.ShouldBeEquivalentTo(bankAccountTypeList[Constants.FirstItem]);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
