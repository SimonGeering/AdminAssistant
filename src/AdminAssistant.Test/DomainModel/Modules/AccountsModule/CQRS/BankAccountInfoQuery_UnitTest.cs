#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.AccountsModule;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;
using AdminAssistant.Modules.AccountsModule.Queries;

namespace AdminAssistant.Test.DomainModel.Modules.AccountsModule.CQRS;

public sealed class BankAccountInfoQuery_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_BankAccountInfoList()
    {
        // Arrange
        var ownerID = 10;
        var bankAccountInfoList = new List<BankAccountInfo>()
            {
                Factory.BankAccountInfo.WithTestData(10).Build(),
                Factory.BankAccountInfo.WithTestData(20).Build()
            };

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();

        var mockRepository = new Mock<IBankAccountInfoRepository>();
        mockRepository.Setup(x => x.GetListAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(bankAccountInfoList));

        services.AddTransient((sp) => mockRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankAccountInfoQuery(ownerID));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(bankAccountInfoList);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
