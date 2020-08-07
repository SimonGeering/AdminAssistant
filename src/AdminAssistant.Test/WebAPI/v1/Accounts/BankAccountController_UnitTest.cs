#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.DomainModel.Modules.AccountsModule.CQRS;
using Ardalis.Result;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant.WebAPI.v1.Accounts
{
    public class BankAccountController_GetBankAccountById_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_Status200Ok_With_ABankAccount_Given_AnExistingBankAccountID()
        {
            // Arrange
            var bankAccount = Factory.BankAccount.WithTestData(10).Build();

            var services = new ServiceCollection();
            services.AddMockLogging();
            services.AddAutoMapper(typeof(MappingProfile));

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<BankAccountByIDQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<BankAccount>.Success(bankAccount)));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<BankAccountController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<BankAccountController>().GetBankAccountById(bankAccount.BankAccountID).ConfigureAwait(false);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Value.Should().BeNull();

            var result = (OkObjectResult)response.Result;
            result.Value.Should().BeAssignableTo<BankAccountResponseDto>();

            var value = ((BankAccountResponseDto)result.Value);
            value.BankAccountID.Should().Be(bankAccount.BankAccountID);
            value.AccountName.Should().Be(bankAccount.AccountName);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_Status404NotFound_Given_ANonExistentBankAccountID()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMocksOfExternalDependencies();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<BankAccountByIDQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<BankAccount>.NotFound()));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<BankAccountController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<BankAccountController>().GetBankAccountById(10).ConfigureAwait(false);

            // Assert
            response.Result.Should().BeOfType<NotFoundResult>();
            response.Value.Should().BeNull();
        }

    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
