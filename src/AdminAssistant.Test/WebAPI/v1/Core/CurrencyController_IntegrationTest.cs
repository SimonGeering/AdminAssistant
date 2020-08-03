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

namespace AdminAssistant.WebAPI.v1.Core
{
    public class CurrencyController_IntegrationTest : IntegrationTestBase
    {
        private readonly Mock<ICurrencyRepository> mockCurrencyRepository = new Mock<ICurrencyRepository>();

        protected override Action<IServiceCollection> ConfigureTestServices() => services =>
        {
            var result = new List<Currency>() { Factory.Currency.WithTestData(10).Build() };
            mockCurrencyRepository.Setup(x => x.GetListAsync()).Returns(Task.FromResult(result));

            services.AddSingleton(mockCurrencyRepository.Object);
        };

        [Fact]
        [Trait("Category", "Integration")]
        public async Task ReturnAListOfBankAccountType_GivenACallToBankAccountTypeGet()
        {
            // Arrange

            // Act
            var response = await this.HttpClient.GetFromJsonAsync<CurrencyResponseDto[]>("api/v1/core/Currency").ConfigureAwait(false);

            // Assert
            response.Should().NotBeEmpty();
            mockCurrencyRepository.Verify(x => x.GetListAsync(), Times.Once());
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
