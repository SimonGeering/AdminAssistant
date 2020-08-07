#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace AdminAssistant.WebAPI.v1.Accounts
{
    [Collection("SequentialDBBackedTests")]
    public class BankAccountType_Get_Should : IntegrationTestBase
    {
        [Fact]
        [Trait("Category", "Integration")]
        public async Task Return_AllBankAccountTypes_Given_NoParameters()
        {
            // Arrange
            await this.ResetDatabaseAsync().ConfigureAwait(false);

            // Act
            var response = await this.HttpClient.GetFromJsonAsync<BankAccountTypeResponseDto[]>("api/v1/accounts/BankAccountType").ConfigureAwait(false);

            // Assert
            response.Should().HaveCount(2);
            response.Should().ContainSingle(x => x.Description == "Current Account");
            response.Should().ContainSingle(x => x.Description == "Savings Account");
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
