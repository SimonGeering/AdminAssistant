#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule;
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
        await ResetDatabaseAsync();

        var dal = Container.GetRequiredService<IBankAccountRepository>();
        await dal.SaveAsync(new BankAccount()
        {
            BankAccountTypeID = new(BankAccountTypes[Constants.FirstItem].BankAccountTypeID),
            CurrencyID = new(Currencies[Constants.FirstItem].CurrencyID),
            OwnerID = PersonalOwner.OwnerID,
            AccountName = "Acme Bank PLC",
        }, default);
        var acmeBuildingSocietyAccount = await dal.SaveAsync(new BankAccount() { AccountName = "Acme Building Society Account" }, default);

        // Act
        var response = await Container.GetRequiredService<IAdminAssistantWebAPIClient>().GetBankAccountByIdAsync(acmeBuildingSocietyAccount.BankAccountID.Value);

        // Assert
        response.BankAccountID.Should().Be(acmeBuildingSocietyAccount.BankAccountID.Value);
        response.AccountName.Should().Be(acmeBuildingSocietyAccount.AccountName);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
