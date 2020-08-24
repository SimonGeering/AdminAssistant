#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Threading.Tasks;
using AdminAssistant.Infra.DAL.Modules.AccountsModule;
using Ardalis.Result;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant.DomainModel.Modules.AccountsModule.CQRS
{
    public class BankByIDQuery_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_NotFound_GivenANonExistentBankID()
        {
            // Arrange
            var nonExistentBankID = Constants.UnknownRecordID;

            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAdminAssistantServerSideDomainModel();

            var mockBankRepository = new Mock<IBankRepository>();
            mockBankRepository.Setup(x => x.GetAsync(nonExistentBankID)).Returns(Task.FromResult<Bank>(null!));

            services.AddTransient((sp) => mockBankRepository.Object);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankByIDQuery(nonExistentBankID)).ConfigureAwait(false);

            // Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_OkBank_GivenAnExistingBankID()
        {
            // Arrange
            var bank = Factory.Bank.WithTestData(10).Build();

            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAdminAssistantServerSideDomainModel();

            var mockBankRepository = new Mock<IBankRepository>();
            mockBankRepository.Setup(x => x.GetAsync(bank.BankID)).Returns(Task.FromResult(bank));

            services.AddTransient((sp) => mockBankRepository.Object);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankByIDQuery(bank.BankID)).ConfigureAwait(false);

            // Assert
            result.Status.Should().Be(ResultStatus.Ok);
            result.Value.Should().Be(bank);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
