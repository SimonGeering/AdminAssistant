#pragma warning disable CA1707 // Identifiers should not contain underscores

using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

using AdminAssistant.DAL.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule;

using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant.WebAPI.v1.Accounts
{
    public class BankAccountInfoController_Should : IntegrationTestBase
    {
        private readonly Mock<IBankAccountInfoRepository> mockBankAccountInfoRepository = new Mock<IBankAccountInfoRepository>();

        protected override Action<IServiceCollection> ConfigureTestServices() => services =>
        {
            var result = new List<BankAccountInfo>() { Factory.BankAccountInfo.WithTestData(10).Build() };

            mockBankAccountInfoRepository.Setup(x => x.GetListAsync()).Returns(Task.FromResult(result));

            services.AddSingleton(mockBankAccountInfoRepository.Object);
        };

        [Fact]
        [Trait("Category", "Integration")]
        public async Task ReturnAListOfBankAccountInfo_GivenACallToBankAccountInfoGet()
        {
            // Arrange

            // Act
            var response = await this.HttpClient.GetFromJsonAsync<BankAccountInfoResponseDto[]>("api/v1/accounts/BankAccountInfo").ConfigureAwait(false);

            // Assert
            response.Should().NotBeEmpty();
            mockBankAccountInfoRepository.Verify(x => x.GetListAsync(), Times.Once());
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
