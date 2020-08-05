#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Threading.Tasks;
using AdminAssistant.DAL.Modules.AccountsModule;
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
            services.AddMockLogging();
            services.AddAdminAssistantServerSideDomainModel();

            var mockBankRepository = new Mock<IBankRepository>();
            mockBankRepository.Setup(x => x.GetAsync(nonExistentBankID)).Returns(Task.FromResult<Bank>(null!));

            services.AddTransient((sp) => mockBankRepository.Object);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankByIDQuery(nonExistentBankID)).ConfigureAwait(false);

            // Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores