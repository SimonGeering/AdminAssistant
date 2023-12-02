#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Infra.Providers;
using AdminAssistant.Modules.AccountsModule.UI;
using AdminAssistant.UI.Shared.WebAPIClient.v1;

namespace AdminAssistant.Test.UI.Modules.AccountsModule;

public sealed class AccountsService_UnitTest
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task ShowAnNewBankAccount_WhenOpenedForCreate()
    {
        // Arrange
        ICollection<BankAccountTypeResponseDto> bankAccountTypeList = new List<BankAccountTypeResponseDto>()
        {
            new BankAccountTypeResponseDto { BankAccountTypeID = 1, Description = "Current Account" },
            new BankAccountTypeResponseDto { BankAccountTypeID = 2, Description = "Savings Account" },
        };

        var mockWebAPIClient = new Mock<IAdminAssistantWebAPIClient>();
        mockWebAPIClient.Setup(x => x.GetBankAccountTypeAsync())
            .Returns(Task.FromResult(bankAccountTypeList));

        var services = new ServiceCollection();
        services.AddAdminAssistantUI();
        services.AddMockClientSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient(_ => new Mock<IPdfFileProvider>().Object);
        services.AddTransient(_ => mockWebAPIClient.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IAccountsService>().LoadBankAccountTypesLookupDataAsync();

        // Assert
        result.Should().BeEquivalentTo(new List<BankAccountType>()
        {
            new BankAccountType() { BankAccountTypeID = BankAccountTypeId.Default, Description = string.Empty },
            new BankAccountType { BankAccountTypeID = new(1), Description = "Current Account" },
            new BankAccountType { BankAccountTypeID = new(2), Description = "Savings Account" },
        });
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
