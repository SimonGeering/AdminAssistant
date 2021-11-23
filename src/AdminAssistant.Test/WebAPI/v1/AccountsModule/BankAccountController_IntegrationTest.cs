#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Infra.DAL.Modules.CoreModule;
using AdminAssistant.UI.Shared.WebAPIClient.v1;

namespace AdminAssistant.Test.WebAPI.v1.AccountsModule;

[Collection("SequentialDBBackedTests")]
public class BankAccount_Get_Should : IntegrationTestBase
{
    [Fact]
    [Trait("Category", "Integration")]
    public async Task Return_ABankAccount_Given_BankAccountID()
    {
        // Arrange
        await ResetDatabaseAsync().ConfigureAwait(false);

        var dal = Container.GetRequiredService<IBankAccountRepository>();
        await dal.SaveAsync(new BankAccount()
        {
            BankAccountTypeID = BankAccountTypes.First().BankAccountTypeID,
            CurrencyID = Currencies.First().CurrencyID,
            AccountName = "Acme Bank PLC",
            
        }).ConfigureAwait(false);
        var acmeBuildingSocietyAccount = await dal.SaveAsync(new BankAccount() { AccountName = "Acme Building Society Account" }).ConfigureAwait(false);

        // Act
        var response = await Container.GetRequiredService<IAdminAssistantWebAPIClient>().GetBankAccountByIdAsync(acmeBuildingSocietyAccount.BankAccountID).ConfigureAwait(false);

        // Assert
        response.BankAccountID.Should().Be(acmeBuildingSocietyAccount.BankAccountID);
        response.AccountName.Should().Be(acmeBuildingSocietyAccount.AccountName);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
