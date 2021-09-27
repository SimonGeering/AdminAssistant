#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Infra.DAL.Modules.AccountsModule;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

public class BankAccountTypesQuery_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_BankAccountTypesList()
    {
        // Arrange
        var bankAccountTypes = new List<BankAccountType>()
            {
                Factory.BankAccountType.WithTestData(10).WithDescription("Current Account").Build(),
                Factory.BankAccountType.WithTestData(20).WithDescription("Savings Account").Build(),
            };

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();

        var mockRepository = new Mock<IBankAccountTypeRepository>();
        mockRepository.Setup(x => x.GetListAsync()).Returns(Task.FromResult(bankAccountTypes));

        services.AddTransient((sp) => mockRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankAccountTypesQuery()).ConfigureAwait(false);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(bankAccountTypes);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
