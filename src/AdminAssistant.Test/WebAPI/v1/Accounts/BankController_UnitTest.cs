#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Collections.Generic;
using System.Linq;
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
    public class BankController_BankGetById_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_Ok200Bank_With_ABank_Given_AnExistingBankID()
        {
            // Arrange
            var bank = Factory.Bank.WithTestData(10).Build();

            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAutoMapper(typeof(MappingProfile));

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<BankByIDQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<Bank>.Success(bank)));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<BankController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<BankController>().BankGetById(bank.BankID).ConfigureAwait(false);

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
        public async Task Return_Status404NotFound_Given_ANonExistentBankID()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddMocksOfExternalServerSideDependencies();

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<BankByIDQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<Bank>.NotFound()));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<BankController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<BankController>().BankGetById(10).ConfigureAwait(false);

            // Assert
            response.Result.Should().BeOfType<NotFoundResult>();
            response.Value.Should().BeNull();
        }
    }

    public class BankController_BankGet_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_Status200Ok_WithAListOfBank_Given_NoArguments()
        {
            // Arrange
            var banks = new List<Bank>()
            {
                Factory.Bank.WithTestData(10).WithBankName("Acme Bank PLC").Build(),
                Factory.Bank.WithTestData(20).WithBankName("Acme Building Society").Build()
            };

            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAutoMapper(typeof(MappingProfile));

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<BankQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<IEnumerable<Bank>>.Success(banks)));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<BankController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<BankController>().BankGet().ConfigureAwait(false);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Value.Should().BeNull();

            var result = (OkObjectResult)response.Result;
            result.Value.Should().BeAssignableTo<IEnumerable<BankResponseDto>>();

            var value = ((IEnumerable<BankResponseDto>)result.Value).ToArray();
            value.Should().HaveCount(banks.Count);

            var expected = banks.ToArray();
            for (int i = 0; i < expected.Length; i++)
            {
                value[i].BankID.Should().Be(expected[i].BankID);
                value[i].BankName.Should().Be(expected[i].BankName);
            }
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
