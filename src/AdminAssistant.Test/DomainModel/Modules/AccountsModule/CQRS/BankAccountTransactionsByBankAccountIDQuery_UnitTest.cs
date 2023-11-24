#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;

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

        var bankAccountID = 9;
        var mockBankAccountTransactionRepository = new Mock<IBankAccountTransactionRepository>();
        mockBankAccountTransactionRepository.Setup(x => x.GetListAsync(bankAccountID))
            .Returns(Task.FromResult<List<BankAccountTransaction>>(bankAccountTransactionList));

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
