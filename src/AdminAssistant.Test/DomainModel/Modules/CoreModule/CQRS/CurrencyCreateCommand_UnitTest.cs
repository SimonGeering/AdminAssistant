#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.CoreModule.Commands;
using AdminAssistant.Modules.CoreModule.Infrastructure.DAL;
using ObjectCloner.Extensions;

// https://github.com/marcelltoth/ObjectCloner

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
        services.AddAdminAssistantApplication();

        var mockCurrencyRepository = new Mock<ICurrencyRepository>();
        mockCurrencyRepository.Setup(x => x.SaveAsync(currency, It.IsAny<CancellationToken>()))
            .Returns(() =>
            {
                var result = currency.DeepClone();
                result = result with { CurrencyID = new(30) };
                return Task.FromResult(result);
            });

        services.AddTransient((sp) => mockCurrencyRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new CurrencyCreateCommand(currency));

        // Assert
        result.ShouldNotBeNull();
        result.Status.ShouldBe(ResultStatus.Ok);
        result.ValidationErrors.ShouldBeEmpty();
        result.Value.ShouldNotBeNull();
        result.Value.CurrencyID.Value.ShouldBeGreaterThan(Constants.NewRecordID);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenAnInvalidBank()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();
        services.AddTransient((sp) => new Mock<ICurrencyRepository>().Object);

        var bank = Factory.Currency.WithTestData()
                                   .WithoutASymbol()
                                   .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new CurrencyCreateCommand(bank));

        // Assert
        result.ShouldNotBeNull();
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ValidationErrors.ShouldNotBeEmpty();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
