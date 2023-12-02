#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Queries;

namespace AdminAssistant.Test.DomainModel.Modules.AccountsModule.CQRS;

public sealed class BankQuery_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_BankList()
    {
        // Arrange
        var bankList = new List<Bank>() { Factory.Bank.WithTestData(20).Build() };

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();

        var mockBankRepository = new Mock<IBankRepository>();
        mockBankRepository.Setup(x => x.GetListAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(bankList));

        services.AddTransient((sp) => mockBankRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankQuery());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(bankList);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
