#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Queries;

namespace AdminAssistant.Test.DomainModel.Modules.AccountsModule.CQRS;

public sealed class BankAccountByIDQuery_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_NotFound_GivenANonExistentBankID()
    {
        // Arrange
        var nonExistentBankAccountID = BankAccountId.Default;

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();

        var mockBankRepository = new Mock<IBankAccountRepository>();
        mockBankRepository.Setup(x => x.GetAsync(nonExistentBankAccountID, It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<BankAccount?>(null!));

        services.AddTransient((sp) => mockBankRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankAccountByIDQuery(nonExistentBankAccountID.Value));

        // Assert
        result.Status.Should().Be(ResultStatus.NotFound);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
