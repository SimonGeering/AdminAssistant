#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Xunit;

namespace AdminAssistant.WebAPI.v1.Accounts
{
    public class BankController_Should : IntegrationTestBase
    {
        private readonly Mock<IBankRepository> mockBankRepository = new Mock<IBankRepository>();

        protected override Action<IServiceCollection> ConfigureTestServices() => services =>
        {
            var result = new List<Bank>() { Factory.Bank.WithTestData(10).Build() };

            mockBankRepository.Setup(x => x.GetListAsync()).Returns(Task.FromResult(result));

            services.AddSingleton(mockBankRepository.Object);
        };

        [Fact]
        [Trait("Category", "Integration")]
        public async Task ReturnAListOfBank_GivenACallToBankGet()
        {
            // Arrange

            // Act
            var response = await this.HttpClient.GetFromJsonAsync<BankResponseDto[]>("api/v1/accounts/Bank").ConfigureAwait(false);

            // Assert
            response.Should().NotBeEmpty();
            mockBankRepository.Verify(x => x.GetListAsync(), Times.Once());
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
