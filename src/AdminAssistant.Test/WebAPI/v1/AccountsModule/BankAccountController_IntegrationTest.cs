#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.UI.Shared.WebAPIClient.v1;

namespace AdminAssistant.Test.WebAPI.v1.AccountsModule;

[Collection("SequentialDBBackedTests")]
public sealed class BankAccount_Get_Should : IntegrationTestBase
{
    [Fact(Skip = "WIP while we work out FK changes.")]
    [Trait("Category", "Integration")]
    [SuppressMessage("Usage", "xUnit1004:Test methods should not be skipped", Justification = "WIP while we work out FK changes.")]
    public async Task Return_ABankAccount_Given_BankAccountID()
    {
        // Arrange
        await ResetDatabaseAsync().ConfigureAwait(false);

        var dal = Container.GetRequiredService<IBankAccountRepository>();
        await dal.SaveAsync(new BankAccount()
        {
            BankAccountTypeID = BankAccountTypes.First().BankAccountTypeID,
            CurrencyID = Currencies.First().CurrencyID,
            OwnerID = PersonalOwner.OwnerID,
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
