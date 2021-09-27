#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.UI.Shared.WebAPIClient.v1;

namespace AdminAssistant.UI.Modules.AccountsModule;

public class AccountsService_UnitTest
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
        services.AddTransient((sp) => mockWebAPIClient.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IAccountsService>().LoadBankAccountTypesLookupDataAsync().ConfigureAwait(false);

        // Assert
        result.Should().BeEquivalentTo(new List<BankAccountType>()
            {
                new BankAccountType() { BankAccountTypeID = Constants.UnknownRecordID, Description = string.Empty },
                new BankAccountType { BankAccountTypeID = 1, Description = "Current Account" },
                new BankAccountType { BankAccountTypeID = 2, Description = "Savings Account" },
            });
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
