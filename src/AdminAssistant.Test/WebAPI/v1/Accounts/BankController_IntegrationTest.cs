#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdminAssistant.WebAPI.v1.Accounts
{
    [Collection("SequentialDBBackedTests")]
    public class Bank_Get_Should : IntegrationTestBase
    { 
        [Fact]
        [Trait("Category", "Integration")]
        public async Task Return_ABank_Given_BankID()
        {
            // Arrange
            await this.ResetDatabaseAsync().ConfigureAwait(false);

            var dal = this.Container.GetService<IBankRepository>();
            await dal.SaveAsync(new Bank() { BankName = "Acme Bank PLC" }).ConfigureAwait(false);
            var acmeBuildingSociety = await dal.SaveAsync(new Bank() { BankName = "Acme Building Society" }).ConfigureAwait(false);

            // Act
            var response = await this.Container.GetService<IAdminAssistantWebAPIClient>().GetBankByIdAsync(acmeBuildingSociety.BankID).ConfigureAwait(false);

            // Assert
            response.BankID.Should().Be(acmeBuildingSociety.BankID);
            response.BankName.Should().Be(acmeBuildingSociety.BankName);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public async Task Return_AllBanks_Given_NoParameters()
        {
            // Arrange
            await this.ResetDatabaseAsync().ConfigureAwait(false);

            var dal = this.Container.GetService<IBankRepository>();
            var acmeBankPLC = await dal.SaveAsync(new Bank() { BankName = "Acme Bank PLC" }).ConfigureAwait(false);
            var acmeBuildingSociety = await dal.SaveAsync(new Bank() { BankName = "Acme Building Society" }).ConfigureAwait(false);

            // Act
            var response = await this.Container.GetService<IAdminAssistantWebAPIClient>().GetBankAsync().ConfigureAwait(false);

            // Assert
            response.Should().ContainSingle(x =>x.BankID == acmeBankPLC.BankID && x.BankName == acmeBankPLC.BankName);
            response.Should().ContainSingle(x => x.BankID == acmeBuildingSociety.BankID && x.BankName == acmeBuildingSociety.BankName);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
