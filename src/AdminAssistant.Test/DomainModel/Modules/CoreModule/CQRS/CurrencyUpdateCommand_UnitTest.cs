#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.CoreModule.Commands;
using AdminAssistant.Modules.CoreModule.Infrastructure.DAL;

namespace AdminAssistant.Test.DomainModel.Modules.CoreModule.CQRS;

public sealed class BankUpdateCommand_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task SaveAndReturn_APersistedBank_GivenAValidBank()
    {
        // Arrange
        var currency = Factory.Currency.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();

        var mockRepository = new Mock<ICurrencyRepository>();
        mockRepository.Setup(x => x.SaveAsync(currency, It.IsAny<CancellationToken>()))
                      .Returns(Task.FromResult(currency));

        services.AddTransient((sp) => mockRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new CurrencyUpdateCommand(currency));

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

        var currency = Factory.Currency.WithTestData()
                                       .WithoutASymbol()
                                       .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new CurrencyUpdateCommand(currency));

        // Assert
        result.ShouldNotBeNull();
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ValidationErrors.ShouldNotBeEmpty();
    }
    // TODO: Add test for CurrencyUpdateCommand where CurrencyID not in ICurrencyRepository
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
