#pragma warning disable CA1707 // Identifiers should not contain underscores

using System;
using System.Collections.Generic;
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
    public class BankAccountInfoController_Should : IntegrationTestBase
    {
        [Fact(Skip="WIP Unit test mapping domain to entity")]
        [Trait("Category", "Integration")]
        public async Task ReturnAListOfBankAccountInfo_GivenACallToBankAccountInfoGet()
        {
            // Arrange
            await this.ResetDatabaseAsync().ConfigureAwait(false);

            var dal = this.Container.GetService<IBankAccountRepository>();

            var acmeSavingsAccount = Factory.BankAccount
                .WithTestData()
                .WithAccountName("Acme Savings Account")
                .Build();

            await dal.SaveAsync(acmeSavingsAccount).ConfigureAwait(false);

            //await dal.SaveAsync(Factory.BankAccount.WithTestData()
            //                                       .WithAccountName("Acme Savings Account")
            //                                       .Build()).ConfigureAwait(false);

            // Act
            var response = await this.HttpClient.GetFromJsonAsync<BankAccountInfoResponseDto[]>("api/v1/accounts/BankAccountInfo").ConfigureAwait(false);

            // Assert
            response.Should().ContainSingle(x => x.AccountName == acmeSavingsAccount.AccountName);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
