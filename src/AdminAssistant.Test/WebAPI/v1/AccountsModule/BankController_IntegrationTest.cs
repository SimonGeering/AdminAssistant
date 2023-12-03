#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

namespace AdminAssistant.Test.WebAPI.v1.AccountsModule;

[Collection("SequentialDBBackedTests")]
public sealed class Bank_Post_Should : IntegrationTestBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task Return_ANewlyCreatedBank_Given_AValidBank()
    {
        // Arrange
        await ResetDatabaseAsync();

        var request = new BankCreateRequestDto() { BankName = "Acme Bank" };

        // Act
        var response = await Container.GetRequiredService<IAdminAssistantWebAPIClient>().PostBankAsync(request);

        // Assert
        response.BankID.Should().BeGreaterThan(0);
        response.BankName.Should().Be(request.BankName);
    }
}

[Collection("SequentialDBBackedTests")]
public sealed class Bank_Put_Should : IntegrationTestBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task Return_ANewlyUpdatedBank_Given_AValidExistingBank()
    {
        // Arrange
        await ResetDatabaseAsync();

        var dal = Container.GetRequiredService<IBankRepository>();
        var acmeBank = await dal.SaveAsync(new Bank() { BankName = new("Acme Bank") }, default);

        var request = new BankUpdateRequestDto()
        {
            BankID = acmeBank.BankID.Value,
            BankName = "Acme UK Bank"
        };

        // Act
        var response = await Container.GetRequiredService<IAdminAssistantWebAPIClient>().PutBankAsync(request);

        // Assert
        response.BankID.Should().Be(request.BankID);
        response.BankName.Should().Be(request.BankName);
    }
}

[Collection("SequentialDBBackedTests")]
public class Bank_Get_Should : IntegrationTestBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task Return_ABank_Given_BankID()
    {
        // Arrange
        await ResetDatabaseAsync();

        var dal = Container.GetRequiredService<IBankRepository>();
        await dal.SaveAsync(new Bank() { BankName = new("Acme Bank PLC") }, default);
        var acmeBuildingSociety = await dal.SaveAsync(new Bank() { BankName = new("Acme Building Society") }, default);

        // Act
        var response = await Container.GetRequiredService<IAdminAssistantWebAPIClient>().GetBankByIdAsync(acmeBuildingSociety.BankID.Value);

        // Assert
        response.BankID.Should().Be(acmeBuildingSociety.BankID.Value);
        response.BankName.Should().Be(acmeBuildingSociety.BankName.Value);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task Return_AllBanks_Given_NoParameters()
    {
        // Arrange
        await ResetDatabaseAsync();

        var dal = Container.GetRequiredService<IBankRepository>();
        var acmeBankPLC = await dal.SaveAsync(new Bank() { BankName = new("Acme Bank PLC") }, default);
        var acmeBuildingSociety = await dal.SaveAsync(new Bank() { BankName = new("Acme Building Society") }, default);

        // Act
        var response = await Container.GetRequiredService<IAdminAssistantWebAPIClient>().GetBankAsync();

        // Assert
        response.Should().ContainSingle(x => x.BankID == acmeBankPLC.BankID.Value && x.BankName == acmeBankPLC.BankName.Value);
        response.Should().ContainSingle(x => x.BankID == acmeBuildingSociety.BankID.Value && x.BankName == acmeBuildingSociety.BankName.Value);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
