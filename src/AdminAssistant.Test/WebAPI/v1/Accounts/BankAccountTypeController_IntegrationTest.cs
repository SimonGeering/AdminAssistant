#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using System;

namespace AdminAssistant.WebAPI.v1.Accounts
{
    public class BankAccountTypeController_Should : IntegrationTestBase
    {
        [Fact]
        [Trait("Category", "Integration")]
        public async Task ReturnAListOfBankAccountType_GivenACallToBankAccountTypeGet()
        {
            // Arrange
            await this.ResetDatabaseAsync().ConfigureAwait(false);

            // Act
            var response = await this.HttpClient.GetFromJsonAsync<BankAccountTypeResponseDto[]>("api/v1/accounts/BankAccountType").ConfigureAwait(false);

            // Assert
            response.Should().NotBeEmpty();
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
