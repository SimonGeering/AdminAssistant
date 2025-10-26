#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Modules.CoreModule;
using AdminAssistant.Modules.CoreModule.UI;
using AdminAssistant.WebAPI.v1.CoreModule;
using AdminAssistant.WebAPIClient.v1.CoreModule;

namespace AdminAssistant.Test.UI.Modules.CoreModule;

public sealed class CoreService_GetCurrencyListAsync
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task ReturnAListOfCurrency_IncludingADefault()
    {
        // Arrange
        IEnumerable<CurrencyResponseDto> currencyList = new List<CurrencyResponseDto>()
            {
                new CurrencyResponseDto { CurrencyID = 1, Symbol = "GBP", DecimalFormat = "2.2-2" },
                new CurrencyResponseDto { CurrencyID = 2, Symbol = "EUR", DecimalFormat = "2.2-2" },
                new CurrencyResponseDto { CurrencyID = 3, Symbol = "USD", DecimalFormat = "2.2-2" },
            };

        var mockWebAPIClient = new Mock<ICurrencyApiClient>();
        mockWebAPIClient.Setup(x => x.GetCurrenciesAsync())
            .Returns(Task.FromResult(currencyList));

        var services = new ServiceCollection();
        services.AddAdminAssistantUI();
        services.AddMockClientSideLogging();
        services.AddTransient((sp) => mockWebAPIClient.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<ICoreService>().GetCurrencyListAsync();

        // Assert
        result.ShouldBeEquivalentTo(new List<Currency>()
            {
                new Currency() { CurrencyID = CurrencyId.Default, Symbol = string.Empty, DecimalFormat = string.Empty },
                new Currency { CurrencyID = new CurrencyId(1), Symbol = "GBP", DecimalFormat = "2.2-2" },
                new Currency { CurrencyID = new CurrencyId(2), Symbol = "EUR", DecimalFormat = "2.2-2" },
                new Currency { CurrencyID = new CurrencyId(3), Symbol = "USD", DecimalFormat = "2.2-2" },
            });
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
