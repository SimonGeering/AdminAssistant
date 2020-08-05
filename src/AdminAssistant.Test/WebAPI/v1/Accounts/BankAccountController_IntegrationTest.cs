#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace AdminAssistant.WebAPI.v1.Accounts
{
    [Collection("SequentialDBBackedTests")]
    public class BankAccountController_Should : IntegrationTestBase
    {
        [Fact(Skip = "WIP")]
        [Trait("Category", "Integration")]
        public async Task ReturnAListOfBanks_GivenACallToBankAccountGet()
        {
            // Arrange
            await this.ResetDatabaseAsync().ConfigureAwait(false);

            // Act
            var response = await this.HttpClient.GetFromJsonAsync<BankAccountResponseDto[]>("api/v1/accounts/BankAccount").ConfigureAwait(false);

            // Assert
            response.Should().NotBeEmpty();
            response.Should().Contain(x => x.BankAccountID == 10);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
