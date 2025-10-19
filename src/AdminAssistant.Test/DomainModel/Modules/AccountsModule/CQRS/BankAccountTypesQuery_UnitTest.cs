#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;
using AdminAssistant.Modules.AccountsModule.Queries;

namespace AdminAssistant.Test.DomainModel.Modules.AccountsModule.CQRS;

public sealed class BankAccountTypesQuery_Should
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
        services.AddAdminAssistantApplication();

        var mockRepository = new Mock<IBankAccountTypeRepository>();
        mockRepository.Setup(x => x.GetListAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(bankAccountTypes));

        services.AddTransient((sp) => mockRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankAccountTypesQuery());

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(bankAccountTypes);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
