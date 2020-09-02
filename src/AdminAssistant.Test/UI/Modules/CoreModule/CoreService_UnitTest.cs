#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.UI.Shared.WebAPIClient.v1;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant.UI.Modules.CoreModule
{
    public class CoreService_GetCurrencyListAsync
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task ReturnAListOfCurrency_IncludingADefault()
        {
            await Task.Delay(1000).ConfigureAwait(false);

            // Arrange
            ICollection<CurrencyResponseDto> currencyList = new List<CurrencyResponseDto>()
            {
                new CurrencyResponseDto { CurrencyID = 1, Symbol = "GBP", DecimalFormat = "2.2-2" },
                new CurrencyResponseDto { CurrencyID = 2, Symbol = "EUR", DecimalFormat = "2.2-2" },
                new CurrencyResponseDto { CurrencyID = 3, Symbol = "USD", DecimalFormat = "2.2-2" },
            };

            var mockWebAPIClient = new Mock<IAdminAssistantWebAPIClient>();
            mockWebAPIClient.Setup(x => x.GetCurrencyAsync())
                .Returns(Task.FromResult(currencyList));

            var services = new ServiceCollection();
            services.AddAdminAssistantUI();
            services.AddMockClientSideLogging();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient((sp) => mockWebAPIClient.Object); 

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<ICoreService>().GetCurrencyListAsync().ConfigureAwait(false);

            // Assert
            result.Should().BeEquivalentTo(new List<Currency>()
            {
                new Currency() { CurrencyID = 0, Symbol = string.Empty, DecimalFormat = string.Empty },
                new Currency { CurrencyID = 1, Symbol = "GBP", DecimalFormat = "2.2-2" },
                new Currency { CurrencyID = 2, Symbol = "EUR", DecimalFormat = "2.2-2" },
                new Currency { CurrencyID = 3, Symbol = "USD", DecimalFormat = "2.2-2" },
            });
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
