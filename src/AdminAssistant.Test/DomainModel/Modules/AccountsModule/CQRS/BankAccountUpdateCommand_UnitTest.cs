#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.AccountsModule.Commands;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;

namespace AdminAssistant.Test.DomainModel.Modules.AccountsModule.CQRS;

public sealed class BankAccountUpdateCommand_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task SaveAndReturn_APersistedBankAccount_GivenAValidBankAccount()
    {
        // Arrange
        var bankAccount = Factory.BankAccount.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();

        var mockBankAccountRepository = new Mock<IBankAccountRepository>();
        mockBankAccountRepository.Setup(x => x.SaveAsync(bankAccount, It.IsAny<CancellationToken>()))
                                 .Returns(Task.FromResult(bankAccount));

        services.AddTransient((sp) => mockBankAccountRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankAccountUpdateCommand(bankAccount));

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Ok);
        result.ValidationErrors.Should().BeEmpty();
        result.Value.Should().NotBeNull();
        result.Value.BankAccountID.Value.Should().BeGreaterThan(Constants.NewRecordID);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenAnInvalidBankAccount()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();
        services.AddTransient((sp) => new Mock<IBankAccountRepository>().Object);

        var bankAccount = Factory.BankAccount.WithTestData()
                                             .WithAccountName(string.Empty)
                                             .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankAccountUpdateCommand(bankAccount));

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ValidationErrors.Should().NotBeEmpty();
    }
    // TODO: Add test for BankAccountUpdateCommand where BankAccountID not in IBankAccountRepository
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
