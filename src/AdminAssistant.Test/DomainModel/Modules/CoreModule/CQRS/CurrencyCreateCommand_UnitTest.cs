#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.CoreModule.CQRS;
using AdminAssistant.Infra.DAL.Modules.CoreModule;
using ObjectCloner.Extensions; // https://github.com/marcelltoth/ObjectCloner

namespace AdminAssistant.Test.DomainModel.Modules.CoreModule.CQRS;

public sealed class CurrencyCreateCommand_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_APersistedCurrency_GivenAValidCurrency()
    {
        // Arrange
        var currency = Factory.Currency.WithTestData().Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();

        var mockCurrencyRepository = new Mock<ICurrencyRepository>();
        mockCurrencyRepository.Setup(x => x.SaveAsync(currency))
            .Returns(() =>
            {
                var result = currency.DeepClone();
                result = result with { CurrencyID = 30 };
                return Task.FromResult(result);
            });

        services.AddTransient((sp) => mockCurrencyRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new CurrencyCreateCommand(currency)).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Ok);
        result.ValidationErrors.Should().BeEmpty();
        result.Value.Should().NotBeNull();
        result.Value.CurrencyID.Should().BeGreaterThan(Constants.NewRecordID);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenAnInvalidBank()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddTransient((sp) => new Mock<ICurrencyRepository>().Object);

        var bank = Factory.Currency.WithTestData()
                                   .WithoutASymbol()
                                   .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new CurrencyCreateCommand(bank)).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ValidationErrors.Should().NotBeEmpty();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
