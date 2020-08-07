#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace AdminAssistant.WebAPI.v1.Accounts
{
    [Collection("SequentialDBBackedTests")]
    public class BankAccount_Get_Should : IntegrationTestBase
    {
        [Fact(Skip = "WIP")]
        [Trait("Category", "Integration")]
        public async Task Return_ABankAccount_Given_BankAccountID()
        {
            // Arrange
            int bankAccountID = 10;
            await this.ResetDatabaseAsync().ConfigureAwait(false);

            // Act
            var response = await this.HttpClient.GetFromJsonAsync<BankAccountResponseDto[]>($"api/v1/accounts/BankAccount/{bankAccountID}").ConfigureAwait(false);

            // Assert
            response.Should().NotBeEmpty();
            response.Should().Contain(x => x.BankAccountID == 10);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
