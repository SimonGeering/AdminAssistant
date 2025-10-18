#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;
using AdminAssistant.Shared;
using MappingProfile = AdminAssistant.Infrastructure.MappingProfile;

namespace AdminAssistant.Test.Infra.DAL.Modules.AccountsModule;

public sealed class BankAccountRepository_UnitTest
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
            .Returns(data.BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient(_ => new Mock<IDateTimeProvider>().Object);
        services.AddTransient(_ => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra();
        services.AddTransient((sp) => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountRepository>().GetListAsync(CancellationToken.None);

        // Assert
        result.Count.ShouldBe(bankAccountList.Count);
        result.ShouldBeEquivalentTo(bankAccountList);
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
            .Returns(data.BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient(_ => new Mock<IDateTimeProvider>().Object);
        services.AddTransient(_ => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra();
        services.AddTransient(_ => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IBankAccountRepository>()
            .GetAsync(bankAccountList[Constants.FirstItem].BankAccountID, CancellationToken.None);

        // Assert
        result.ShouldBeEquivalentTo(bankAccountList[Constants.FirstItem]);
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

        var mockBankAccounts = mapper.Map<IList<BankAccountEntity>>(bankAccountList).BuildMockDbSet();
        mockBankAccounts.Setup(x => x.Add(It.IsAny<BankAccountEntity>()));

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.BankAccounts).Returns(mockBankAccounts.Object);

        var services = new ServiceCollection();
        services.AddMockDateTimeProvider();
        services.AddMockUserContextProvider();
        services.AddMockDbContext(mockDbContext);
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddAdminAssistantServerSideInfra();

        var newBankAccountToSave = Factory.BankAccount.WithAccountName("TestNewBankAccountToSave").Build();

        // Act
        await services.BuildServiceProvider().GetRequiredService<IBankAccountRepository>().SaveAsync(newBankAccountToSave, default);

        // Assert
        mockBankAccounts.Verify(x => x.Add(It.Is<BankAccountEntity>((arg) => IsValidForInsert(arg))), Times.Once());
        mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    //TODO: UpdatesAuditing_WhenSavingAnExistingBankAccount

    private static bool IsValidForInsert(BankAccountEntity bankAccountToSave)
    {
        bankAccountToSave.BankAccountID.ShouldBe(Constants.NewRecordID);
        bankAccountToSave.Audit.ShouldNotBeNull();

        bankAccountToSave.Audit.CreatedBy.ShouldNotBeNullOrEmpty();
        bankAccountToSave.Audit.CreatedOn.ShouldNotBe(default);

        return true;
    }

    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "WIP")]
    private static bool IsValidForUpdate(BankAccountEntity bankAccountToSave)
    {
        bankAccountToSave.BankAccountID.ShouldNotBe(Constants.NewRecordID);
        bankAccountToSave.Audit.ShouldNotBeNull();

        return true;
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
