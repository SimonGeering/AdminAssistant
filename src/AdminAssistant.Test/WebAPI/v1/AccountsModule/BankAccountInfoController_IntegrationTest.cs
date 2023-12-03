#if DEBUG // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
#pragma warning disable CA1707 // Identifiers should not contain underscores

namespace AdminAssistant.Test.WebAPI.v1.AccountsModule;

[Collection("SequentialDBBackedTests")]
public sealed class BankAccountInfo_Get_Should : IntegrationTestBase
{
    #pragma warning disable S125 // Sections of code should not be commented out
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
    #pragma warning restore S125 // Sections of code should not be commented out
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
#endif // quick and dirty fix for #85 category filtering breaking CI Unit Test run.
