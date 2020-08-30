#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Threading.Tasks;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdminAssistant.WebAPI.v1.Accounts
{
    [Collection("SequentialDBBackedTests")]
    public class BankAccount_Get_Should : IntegrationTestBase
    {
        //[Fact(Skip = "WIP")]
        //[Trait("Category", "Integration")]
        //public async Task Return_ABankAccount_Given_BankAccountID()
        //{
        //    // Arrange
        //    int bankAccountID = 10;
        //    await this.ResetDatabaseAsync().ConfigureAwait(false);

        //    // Act
        //    var response = await this.Container.GetRequiredService<IAdminAssistantWebAPIClient>().GetBankAccountByIdAsync(bankAccountID).ConfigureAwait(false);

        //    // Assert
        //    response.BankAccountID.Should().Be(bankAccountID);
        //}
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
