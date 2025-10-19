#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.AccountsModule.Commands;
using AdminAssistant.Modules.AccountsModule.Infrastructure.DAL;
using ObjectCloner.Extensions; // https://github.com/marcelltoth/ObjectCloner

namespace AdminAssistant.Test.DomainModel.Modules.AccountsModule.CQRS;

public sealed class BankAccountCreateCommand_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_APersistedBankAccount_GivenAValidBankAccount()
    {
        // Arrange
        var bankAccount = Factory.BankAccount.WithTestData().Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();

        var mockBankAccountRepository = new Mock<IBankAccountRepository>();
        mockBankAccountRepository.Setup(x => x.SaveAsync(bankAccount, It.IsAny<CancellationToken>()))
            .Returns(() =>
            {
                var result = bankAccount.DeepClone();
                result = result with { BankAccountID = new(30) };
                return Task.FromResult(result);
            });

        services.AddTransient((sp) => mockBankAccountRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankAccountCreateCommand(bankAccount));

        // Assert
        result.ShouldNotBeNull();
        result.Status.ShouldBe(ResultStatus.Ok);
        result.ValidationErrors.ShouldBeEmpty();
        result.Value.ShouldNotBeNull();
        result.Value.BankAccountID.Value.ShouldBeGreaterThan(Constants.NewRecordID);
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
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankAccountCreateCommand(bankAccount));

        // Assert
        result.ShouldNotBeNull();
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ValidationErrors.ShouldNotBeEmpty();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
