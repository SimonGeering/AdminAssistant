#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;
using AdminAssistant.Infra.Providers;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace AdminAssistant.Infra.DAL.Modules.AccountsModule;

public class BankAccountRepository_UnitTest
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_PopulatedBankAccountList_WhenDatabaseHasData()
    {
        // Arrange
        var mapper = new ServiceCollection().AddAutoMapper(typeof(MappingProfile)).BuildServiceProvider().GetRequiredService<IMapper>();
        var bankAccountList = new List<BankAccount>()
            {
                Factory.BankAccount.WithTestData(10).Build(),
                Factory.BankAccount.WithTestData(20).Build()
            };
        var data = mapper.Map<IList<BankAccountEntity>>(bankAccountList);

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.BankAccounts)
            .Returns(data.AsQueryable().BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient((sp) => new Mock<IDateTimeProvider>().Object);
        services.AddTransient((sp) => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra(new ConfigurationSettings() { ConnectionString = "FakeConnectionString", DatabaseProvider = "SQLServerLocalDB" });
        services.AddTransient((sp) => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountRepository>().GetListAsync().ConfigureAwait(false);

        // Assert
        result.Should().HaveCount(bankAccountList.Count);
        result.Should().BeEquivalentTo(bankAccountList);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_ABankAccount_WhenDatabaseContainsAnItemWithTheGivenID()
    {
        // Arrange
        var mapper = new ServiceCollection().AddAutoMapper(typeof(MappingProfile)).BuildServiceProvider().GetRequiredService<IMapper>();

        var bankAccountList = new List<BankAccount>()
            {
                Factory.BankAccount.WithTestData(10).Build(),
                Factory.BankAccount.WithTestData(20).Build()
            };
        var data = mapper.Map<IList<BankAccountEntity>>(bankAccountList);

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.BankAccounts)
            .Returns(data.AsQueryable().BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient((sp) => new Mock<IDateTimeProvider>().Object);
        services.AddTransient((sp) => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra(new ConfigurationSettings() { ConnectionString = "FakeConnectionString", DatabaseProvider = "SQLServerLocalDB" });
        services.AddTransient((sp) => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountRepository>().GetAsync(bankAccountList.First().BankAccountID).ConfigureAwait(false);

        // Assert
        result.Should().BeEquivalentTo(bankAccountList.First());
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task AddsAuditing_WhenSavingNewBankAccount()
    {
        // Arrange
        var mapper = new ServiceCollection().AddAutoMapper(typeof(MappingProfile)).BuildServiceProvider().GetRequiredService<IMapper>();
        var bankAccountList = new List<BankAccount>()
            {
                Factory.BankAccount.WithTestData(10).Build(),
                Factory.BankAccount.WithTestData(20).Build()
            };

        var mockBankAccounts = mapper.Map<IList<BankAccountEntity>>(bankAccountList).AsQueryable().BuildMockDbSet();
        mockBankAccounts.Setup(x => x.Add(It.IsAny<BankAccountEntity>()));

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.BankAccounts).Returns(mockBankAccounts.Object);

        var services = new ServiceCollection();
        services.AddMockDateTimeProvider();
        services.AddMockUserContextProvider();
        services.AddMockDbContext(mockDbContext);
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddAdminAssistantServerSideInfra(new ConfigurationSettings() { ConnectionString = "FakeConnectionString", DatabaseProvider = "SQLServerLocalDB" });

        var newBankAccountToSave = Factory.BankAccount.WithAccountName("TestNewBankAccountToSave").Build();

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountRepository>().SaveAsync(newBankAccountToSave).ConfigureAwait(false);

        // Assert
        mockBankAccounts.Verify(x => x.Add(It.Is<BankAccountEntity>((arg) => IsValidForInsert(arg))), Times.Once());
        mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    //UpdatesAuditing_WhenSavingAnExistingBankAccount

    private bool IsValidForInsert(BankAccountEntity bankAccountToSave)
    {
        bankAccountToSave.BankAccountID.Should().Be(Constants.NewRecordID);
        bankAccountToSave.Audit.Should().NotBeNull();

        bankAccountToSave.Audit.CreatedBy.Should().NotBeNullOrEmpty();
        bankAccountToSave.Audit.CreatedOn.Should().NotBe(default);

        return true;
    }

    private bool IsValidForUpdate(BankAccountEntity bankAccountToSave)
    {
        bankAccountToSave.BankAccountID.Should().NotBe(Constants.NewRecordID);
        bankAccountToSave.Audit.Should().NotBeNull();

        return true;
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
