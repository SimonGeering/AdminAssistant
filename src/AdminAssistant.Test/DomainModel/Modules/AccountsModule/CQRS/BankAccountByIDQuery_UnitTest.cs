#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using Ardalis.Result;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;

public class BankAccountByIDQuery_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_NotFound_GivenANonExistentBankID()
    {
        // Arrange
        var nonExistentBankAccountID = Constants.UnknownRecordID;

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();

        var mockBankRepository = new Mock<IBankAccountRepository>();
        mockBankRepository.Setup(x => x.GetAsync(nonExistentBankAccountID)).Returns(Task.FromResult<BankAccount?>(null!));

        services.AddTransient((sp) => mockBankRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankAccountByIDQuery(nonExistentBankAccountID)).ConfigureAwait(false);

        // Assert
        result.Status.Should().Be(ResultStatus.NotFound);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
