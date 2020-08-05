#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Net.Http.Json;
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdminAssistant.WebAPI.v1.Accounts
{
    [Collection("SequentialDBBackedTests")]
    public class BankController_IntegrationTest : IntegrationTestBase
    { 
        [Fact]
        [Trait("Category", "Integration")]
        public async Task ReturnABank_GivenACallToBankGetByID()
        {
            // Arrange
            await this.ResetDatabaseAsync().ConfigureAwait(false);

            var dal = this.Container.GetService<IBankRepository>();
            await dal.SaveAsync(new Bank() { BankName = "Acme Bank PLC" }).ConfigureAwait(false);
            var acmeBuildingSociety = await dal.SaveAsync(new Bank() { BankName = "Acme Building Society" }).ConfigureAwait(false);

            // Act
            var response = await this.HttpClient.GetFromJsonAsync<BankResponseDto[]>($"api/v1/accounts/Bank/{acmeBuildingSociety.BankID}").ConfigureAwait(false);

            // Assert
            response.Should().OnlyContain(x => x.BankID == acmeBuildingSociety.BankID && x.BankName == acmeBuildingSociety.BankName);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task ReturnAListOfBank_GivenACallToBankGet()
        {
            // Arrange
            await this.ResetDatabaseAsync().ConfigureAwait(false);

            var dal = this.Container.GetService<IBankRepository>();
            var acmeBankPLC = await dal.SaveAsync(new Bank() { BankName = "Acme Bank PLC" }).ConfigureAwait(false);
            var acmeBuildingSociety = await dal.SaveAsync(new Bank() { BankName = "Acme Building Society" }).ConfigureAwait(false);

            // Act
            var response = await this.HttpClient.GetFromJsonAsync<BankResponseDto[]>("api/v1/accounts/Bank").ConfigureAwait(false);

            // Assert
            response.Should().ContainSingle(x =>x.BankID == acmeBankPLC.BankID && x.BankName == acmeBankPLC.BankName);
            response.Should().ContainSingle(x => x.BankID == acmeBuildingSociety.BankID && x.BankName == acmeBuildingSociety.BankName);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
