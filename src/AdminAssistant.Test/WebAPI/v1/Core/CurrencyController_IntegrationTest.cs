#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Threading.Tasks;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdminAssistant.WebAPI.v1.Core
{
    [Collection("SequentialDBBackedTests")]
    public class Currency_Get_Should : IntegrationTestBase
    {
        [Fact]
        [Trait("Category", "Integration")]
        public async Task Return_AllCurrencies_Given_NoParameters()
        {
            // Arrange
            await this.ResetDatabaseAsync().ConfigureAwait(false);

            // Act
            var response = await this.Container.GetRequiredService<IAdminAssistantWebAPIClient>().GetCurrencyAsync().ConfigureAwait(false);

            // Assert
            response.Should().HaveCount(3);
            response.Should().ContainSingle(x => x.Symbol == "GBP");
            response.Should().ContainSingle(x => x.Symbol == "EUR");
            response.Should().ContainSingle(x => x.Symbol == "USD");
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
