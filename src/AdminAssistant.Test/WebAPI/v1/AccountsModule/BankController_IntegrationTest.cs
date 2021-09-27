#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.UI.Shared.WebAPIClient.v1;

namespace AdminAssistant.WebAPI.v1.AccountsModule;

[Collection("SequentialDBBackedTests")]
public class Bank_Get_Should : IntegrationTestBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task Return_ABank_Given_BankID()
    {
        // Arrange
        await ResetDatabaseAsync().ConfigureAwait(false);

        var dal = Container.GetRequiredService<IBankRepository>();
        await dal.SaveAsync(new Bank() { BankName = "Acme Bank PLC" }).ConfigureAwait(false);
        var acmeBuildingSociety = await dal.SaveAsync(new Bank() { BankName = "Acme Building Society" }).ConfigureAwait(false);

        // Act
        var response = await Container.GetRequiredService<IAdminAssistantWebAPIClient>().GetBankByIdAsync(acmeBuildingSociety.BankID).ConfigureAwait(false);

        // Assert
        response.BankID.Should().Be(acmeBuildingSociety.BankID);
        response.BankName.Should().Be(acmeBuildingSociety.BankName);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task Return_AllBanks_Given_NoParameters()
    {
        // Arrange
        await ResetDatabaseAsync().ConfigureAwait(false);

        var dal = Container.GetRequiredService<IBankRepository>();
        var acmeBankPLC = await dal.SaveAsync(new Bank() { BankName = "Acme Bank PLC" }).ConfigureAwait(false);
        var acmeBuildingSociety = await dal.SaveAsync(new Bank() { BankName = "Acme Building Society" }).ConfigureAwait(false);

        // Act
        var response = await Container.GetRequiredService<IAdminAssistantWebAPIClient>().GetBankAsync().ConfigureAwait(false);

        // Assert
        response.Should().ContainSingle(x => x.BankID == acmeBankPLC.BankID && x.BankName == acmeBankPLC.BankName);
        response.Should().ContainSingle(x => x.BankID == acmeBuildingSociety.BankID && x.BankName == acmeBuildingSociety.BankName);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
