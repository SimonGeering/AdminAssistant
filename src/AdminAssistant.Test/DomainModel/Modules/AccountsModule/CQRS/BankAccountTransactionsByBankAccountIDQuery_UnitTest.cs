#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;
using AdminAssistant.Modules.AccountsModule.Queries;

namespace AdminAssistant.Test.DomainModel.Modules.AccountsModule.CQRS;

public sealed class BankAccountTransactionsByBankAccountIDQuery_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_BankAccountTransactionsList()
    {
        // Arrange
        var bankAccountTransactionList = new List<BankAccountTransaction>()
        {
            Factory.BankAccountTransaction.WithTestData(10).Build(),
            Factory.BankAccountTransaction.WithTestData(20).Build(),
            Factory.BankAccountTransaction.WithTestData(30).Build()
        };

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();

        var bankAccountID = 9;
        var mockBankAccountTransactionRepository = new Mock<IBankAccountTransactionRepository>();
        mockBankAccountTransactionRepository.Setup(x => x.GetListAsync(bankAccountID, It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(bankAccountTransactionList));

        services.AddTransient((sp) => mockBankAccountTransactionRepository.Object);
        var query = new BankAccountTransactionsByBankAccountIDQuery(bankAccountID);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(query);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(bankAccountTransactionList);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
