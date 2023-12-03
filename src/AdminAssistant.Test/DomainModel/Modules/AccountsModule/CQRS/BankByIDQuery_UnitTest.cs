#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;
using AdminAssistant.Modules.AccountsModule.Queries;

namespace AdminAssistant.Test.DomainModel.Modules.AccountsModule.CQRS;

public sealed class BankByIDQuery_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_NotFound_GivenANonExistentBankID()
    {
        // Arrange
        var nonExistentBankID = BankId.Default;

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();

        var mockBankRepository = new Mock<IBankRepository>();
        mockBankRepository.Setup(x => x.GetAsync(nonExistentBankID, It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Bank?>(null!));

        services.AddTransient((sp) => mockBankRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankByIDQuery(nonExistentBankID.Value));

        // Assert
        result.Status.Should().Be(ResultStatus.NotFound);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_OkBank_GivenAnExistingBankID()
    {
        // Arrange
        var bank = Factory.Bank.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();

        var mockBankRepository = new Mock<IBankRepository>();
        mockBankRepository.Setup(x => x.GetAsync(bank.BankID, It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Bank?>(bank));

        services.AddTransient((sp) => mockBankRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankByIDQuery(bank.BankID.Value));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(bank);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
