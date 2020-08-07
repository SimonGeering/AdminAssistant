#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
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
            var response = await this.HttpClient.GetFromJsonAsync<CurrencyResponseDto[]>("api/v1/core/Currency").ConfigureAwait(false);

            // Assert
            response.Should().HaveCount(3);
            response.Should().ContainSingle(x => x.Symbol == "GBP");
            response.Should().ContainSingle(x => x.Symbol == "EUR");
            response.Should().ContainSingle(x => x.Symbol == "USD");
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
