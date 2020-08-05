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
    public class BankController_GetBankById_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_Ok200Bank_GivenAnExistingBankID()
        {
            // Arrange
            var bank = Factory.Bank.WithTestData(10).Build();

            var services = new ServiceCollection();
            services.AddMockLogging();
            services.AddAutoMapper(typeof(MappingProfile));

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<BankByIDQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<Bank>.Success(bank)));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<BankController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<BankController>().GetBankById(bank.BankID).ConfigureAwait(false);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Value.Should().BeNull();

            var result = (OkObjectResult)response.Result;
            result.Value.Should().BeAssignableTo<BankResponseDto>();

            var value = ((BankResponseDto)result.Value);
            value.BankID.Should().Be(bank.BankID);
            value.BankName.Should().Be(bank.BankName);
        }

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
