#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using Ardalis.Result;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankAccountUpdateCommand_Should
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

            var mockBankAccountRepository = new Mock<IBankAccountRepository>();
            mockBankAccountRepository.Setup(x => x.SaveAsync(bankAccount))
                                     .Returns(Task.FromResult(bankAccount));

            services.AddTransient((sp) => mockBankAccountRepository.Object);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankAccountUpdateCommand(bankAccount)).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultStatus.Ok);
            result.ValidationErrors.Should().BeEmpty();
            result.Value.Should().NotBeNull();
            result.Value.BankAccountID.Should().BeGreaterThan(Constants.NewRecordID);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_ValidationError_GivenAnInvalidBankAccount()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAdminAssistantServerSideDomainModel();
            services.AddTransient((sp) => new Mock<IBankAccountRepository>().Object);

            var bankAccount = Factory.BankAccount.WithTestData()
                                                 .WithAccountName(string.Empty)
                                                 .Build();
            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankAccountUpdateCommand(bankAccount)).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Status.Should().Be(ResultStatus.Invalid);
            result.ValidationErrors.Should().NotBeEmpty();
        }
        // TODO: Add test for BankAccountUpdateCommand where BankAccountID not in IBankAccountRepository
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
