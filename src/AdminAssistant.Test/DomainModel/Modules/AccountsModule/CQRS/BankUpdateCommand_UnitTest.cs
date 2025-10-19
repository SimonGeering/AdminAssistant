#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.AccountsModule.Commands;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

namespace AdminAssistant.Test.DomainModel.Modules.AccountsModule.CQRS;

public sealed class BankUpdateCommand_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task SaveAndReturn_APersistedBank_GivenAValidBank()
    {
        // Arrange
        var bank = Factory.Bank.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();

        var mockBankRepository = new Mock<IBankRepository>();
        mockBankRepository.Setup(x => x.SaveAsync(bank, It.IsAny<CancellationToken>()))
                          .Returns(Task.FromResult(bank));

        services.AddTransient((sp) => mockBankRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankUpdateCommand(bank));

        // Assert
        result.ShouldNotBeNull();
        result.Status.ShouldBe(ResultStatus.Ok);
        result.ValidationErrors.ShouldBeEmpty();
        result.Value.ShouldNotBeNull();
        result.Value.BankID.Value.ShouldBeGreaterThan(Constants.NewRecordID);
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
        services.AddTransient((sp) => new Mock<IBankRepository>().Object);

        var bank = Factory.Bank.WithTestData()
                               .WithBankName(string.Empty)
                               .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankUpdateCommand(bank));

        // Assert
        result.ShouldNotBeNull();
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ValidationErrors.ShouldNotBeEmpty();
    }
    // TODO: Add test for BankUpdateCommand where BankID not in IBankRepository
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
