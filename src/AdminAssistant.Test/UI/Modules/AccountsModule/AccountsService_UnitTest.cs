#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Modules.AccountsModule.UI;
using AdminAssistant.WebAPI.v1.AccountsModule;
using AdminAssistant.WebAPIClient.v1.AccountsModule;

namespace AdminAssistant.Test.UI.Modules.AccountsModule;

public sealed class AccountsService_UnitTest
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task ShowAnNewBankAccount_WhenOpenedForCreate()
    {
        // Arrange
        IEnumerable<BankAccountTypeResponseDto> bankAccountTypeList = new List<BankAccountTypeResponseDto>
        {
            new BankAccountTypeResponseDto { BankAccountTypeID = 1, Description = "Current Account" },
            new BankAccountTypeResponseDto { BankAccountTypeID = 2, Description = "Savings Account" },
        };

        var mockBankAccountTypeApiClient = new Mock<IBankAccountTypeApiClient>();
        mockBankAccountTypeApiClient.Setup(x => x.GetBankAccountTypesAsync(CancellationToken.None))
            .Returns(Task.FromResult(bankAccountTypeList));

        var services = new ServiceCollection();
        services.AddAdminAssistantUI();
        services.AddMockClientSideLogging();
        services.AddTransient(_ => new Mock<IPdfFileProvider>().Object);
        services.AddTransient(_ => mockBankAccountTypeApiClient.Object);
        services.AddTransient(_ => new Mock<IBankAccountApiClient>().Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IAccountsService>().LoadBankAccountTypesLookupDataAsync();

        // Assert
        result.ShouldBeEquivalentTo(new List<BankAccountType>()
        {
            new BankAccountType() { BankAccountTypeID = BankAccountTypeId.Default, Description = string.Empty },
            new BankAccountType { BankAccountTypeID = new(1), Description = "Current Account" },
            new BankAccountType { BankAccountTypeID = new(2), Description = "Savings Account" },
        });
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
