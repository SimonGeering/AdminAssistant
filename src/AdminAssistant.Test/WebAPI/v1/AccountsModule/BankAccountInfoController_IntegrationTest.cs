#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
#pragma warning disable CA1707 // Identifiers should not contain underscores

namespace AdminAssistant.WebAPI.v1.AccountsModule;

[Collection("SequentialDBBackedTests")]
public class BankAccountInfo_Get_Should : IntegrationTestBase
{
    //[Fact(Skip="WIP Unit test mapping domain to entity")]
    //[Trait("Category", "Integration")]
    //public async Task Return_AllBankAccountInfo_Given_NoParameters()
    //{
    //    // Arrange
    //    await this.ResetDatabaseAsync().ConfigureAwait(false);

    //    var dal = this.Container.GetRequiredService<IBankAccountRepository>();

    //    var acmeSavingsAccount = Factory.BankAccount
    //        .WithTestData()
    //        .WithAccountName("Acme Savings Account")
    //        .Build();

    //    await dal.SaveAsync(acmeSavingsAccount).ConfigureAwait(false);

    //    //await dal.SaveAsync(Factory.BankAccount.WithTestData()
    //    //                                       .WithAccountName("Acme Savings Account")
    //    //                                       .Build()).ConfigureAwait(false);

    //    // Act
    //    var response = await this.Container.GetRequiredService<IAdminAssistantWebAPIClient>().GetBankAccountInfoAsync().ConfigureAwait(false);

    //    // Assert
    //    response.Should().ContainSingle(x => x.AccountName == acmeSavingsAccount.AccountName);
    //}
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
