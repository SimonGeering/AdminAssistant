#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.UI.Shared.WebAPIClient.v1;

namespace AdminAssistant.Test.WebAPI.v1.AccountsModule;

[Collection("SequentialDBBackedTests")]
public class BankAccountType_Get_Should : IntegrationTestBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task Return_AllBankAccountTypes_Given_NoParameters()
    {
        // Arrange
        await ResetDatabaseAsync().ConfigureAwait(false);

        // Act
        var response = await Container.GetRequiredService<IAdminAssistantWebAPIClient>().GetBankAccountTypeAsync().ConfigureAwait(false);

        // Assert
        response.Should().HaveCount(2);
        response.Should().ContainSingle(x => x.Description == "Current Account");
        response.Should().ContainSingle(x => x.Description == "Savings Account");
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
