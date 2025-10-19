#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.CoreModule;
using AdminAssistant.Modules.CoreModule.Infrastructure.DAL;
using AdminAssistant.Modules.CoreModule.Queries;

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
        services.AddAdminAssistantApplication();

        var mockRepository = new Mock<ICurrencyRepository>();
        mockRepository.Setup(x => x.GetListAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(currencyList));

        services.AddTransient((sp) => mockRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new CurrenciesQuery());

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(currencyList);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
