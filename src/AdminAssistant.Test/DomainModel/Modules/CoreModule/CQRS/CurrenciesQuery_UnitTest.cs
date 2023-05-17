#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.CoreModule;
using AdminAssistant.DomainModel.Modules.CoreModule.CQRS;
using AdminAssistant.Infra.DAL.Modules.CoreModule;

namespace AdminAssistant.Test.DomainModel.Modules.CoreModule.CQRS;

public sealed class CurrenciesQuery_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_BankList()
    {
        // Arrange
        var currencyList = new List<Currency>()
        {
            Factory.Currency.WithTestData(10).Build(),
            Factory.Currency.WithTestData(20).Build()
        };

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();

        var mockRepository = new Mock<ICurrencyRepository>();
        mockRepository.Setup(x => x.GetListAsync()).Returns(Task.FromResult(currencyList));

        services.AddTransient((sp) => mockRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new CurrenciesQuery()).ConfigureAwait(false);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(currencyList);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
