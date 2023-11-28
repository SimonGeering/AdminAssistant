#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.CoreModule.CQRS;
using AdminAssistant.Infra.DAL.Modules.CoreModule;

namespace AdminAssistant.Test.DomainModel.Modules.CoreModule.CQRS;

public sealed class CurrencyByIDQuery_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_NotFound_GivenANonExistentCurrencyID()
    {
        // Arrange
        var nonExistentCurrencyID = CurrencyId.Default;

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();

        var mockCurrencyRepository = new Mock<ICurrencyRepository>();
        mockCurrencyRepository.Setup(x => x.GetAsync(nonExistentCurrencyID, It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Currency?>(null!));

        services.AddTransient((sp) => mockCurrencyRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new CurrencyByIDQuery(nonExistentCurrencyID.Value));

        // Assert
        result.Status.Should().Be(ResultStatus.NotFound);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_OkCurrency_GivenAnExistingCurrencyID()
    {
        // Arrange
        var currency = Factory.Currency.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();

        var mockCurrencyRepository = new Mock<ICurrencyRepository>();
        mockCurrencyRepository.Setup(x => x.GetAsync(currency.CurrencyID, It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Currency?>(currency));

        services.AddTransient((sp) => mockCurrencyRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new CurrencyByIDQuery(currency.CurrencyID.Value));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(currency);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
