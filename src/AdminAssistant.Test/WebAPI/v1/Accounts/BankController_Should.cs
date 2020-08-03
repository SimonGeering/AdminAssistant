#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Net.Http.Json;
using System.Threading.Tasks;
using AdminAssistant.DAL.EntityFramework.Model.Accounts;
using FluentAssertions;
using Xunit;

namespace AdminAssistant.WebAPI.v1.Accounts
{
    public class BankController_Should : IntegrationTestBase
    {
        [Fact]
        [Trait("Category", "Integration")]
        public async Task ReturnAListOfBank_GivenACallToBankGet()
        {
            // Arrange
            await this.ResetDatabaseAsync().ConfigureAwait(false);

            this.DbContext.Banks.Add(new BankEntity() { BankName = "Acme Bank PLC" });
            this.DbContext.Banks.Add(new BankEntity() { BankName = "Acme Building Society" });

            await this.DbContext.SaveChangesAsync().ConfigureAwait(false);

            // Act
            var response = await this.HttpClient.GetFromJsonAsync<BankResponseDto[]>("api/v1/accounts/Bank").ConfigureAwait(false);

            // Assert
            response.Should().ContainSingle(x => x.BankName == "Acme Bank PLC");
            response.Should().ContainSingle(x => x.BankName == "Acme Building Society");
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
