#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using Ardalis.Result;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant.WebAPI.v1.Accounts
{
    public class BankController_GetBankById_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_Status404NotFound_GivenANonExistentBankID()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMocksOfExternalDependencies();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<BankByIDQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<Bank>.NotFound()));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<BankController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<BankController>().GetBankById(10).ConfigureAwait(false);

            // Assert
            response.Result.Should().BeOfType<NotFoundResult>();
            response.Value.Should().BeNull();
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
