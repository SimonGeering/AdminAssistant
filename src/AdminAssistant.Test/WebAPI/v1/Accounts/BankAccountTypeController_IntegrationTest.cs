#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Threading.Tasks;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
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
            var response = await this.Container.GetRequiredService<IAdminAssistantWebAPIClient>().GetBankAccountTypeAsync().ConfigureAwait(false);

            // Assert
            response.Should().HaveCount(2);
            response.Should().ContainSingle(x => x.Description == "Current Account");
            response.Should().ContainSingle(x => x.Description == "Savings Account");
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
