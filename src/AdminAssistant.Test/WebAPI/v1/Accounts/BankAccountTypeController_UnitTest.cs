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
    public class BankAccountTypeController_Get_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_Status200OK_With_AListOfBankAccountType_Given_NoArguments()
        {
            // Arrange
            var bankAccountTypes = new List<BankAccountType>()
            {
                Factory.BankAccountType.WithTestData(10).WithDescription("Current Account").Build(),
                Factory.BankAccountType.WithTestData(20).WithDescription("Savings Account").Build(),
            };

            var services = new ServiceCollection();
            services.AddMockLogging();
            services.AddAutoMapper(typeof(MappingProfile));

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<BankAccountTypesQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<IEnumerable<BankAccountType>>.Success(bankAccountTypes)));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<BankAccountTypeController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<BankAccountTypeController>().Get().ConfigureAwait(false);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Value.Should().BeNull();

            var result = (OkObjectResult)response.Result;
            result.Value.Should().BeAssignableTo<IEnumerable<BankAccountTypeResponseDto>>();

            var value = ((IEnumerable<BankAccountTypeResponseDto>)result.Value).ToArray();
            value.Should().HaveCount(bankAccountTypes.Count);

            var expected = bankAccountTypes.ToArray();
            for (int i = 0; i < expected.Length; i++)
            {
                value[i].BankAccountTypeID.Should().Be(expected[i].BankAccountTypeID);
                value[i].Description.Should().Be(expected[i].Description);
            }
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
