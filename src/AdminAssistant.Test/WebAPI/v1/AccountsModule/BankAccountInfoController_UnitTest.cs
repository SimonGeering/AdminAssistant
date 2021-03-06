#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Shared;
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

namespace AdminAssistant.WebAPI.v1.AccountsModule
{
    public class BankAccountInfoController_BankAccountInfoGet_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_Status200OK_With_AListOfBankAccountInfo_Given_NoArguments()
        {
            // Arrange
            var bankAccountInfoList = new List<BankAccountInfo>()
            {
                Factory.BankAccountInfo.WithTestData(10).Build(),
                Factory.BankAccountInfo.WithTestData(20).Build()
            };
                        
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<BankAccountInfoQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<IEnumerable<BankAccountInfo>>.Success(bankAccountInfoList)));

            var mockUserContextProvider = new Mock<IUserContextProvider>();
            mockUserContextProvider.Setup(x => x.GetCurrentUser())
                                   .Returns(new User() { UserID = 10, SignOn = "simongeering" });

            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient((sp) => mockUserContextProvider.Object);
            services.AddTransient<BankAccountInfoController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<BankAccountInfoController>().BankAccountInfoGet().ConfigureAwait(false);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Value.Should().BeNull();

            var result = (OkObjectResult)response.Result;
            result.Value.Should().BeAssignableTo<IEnumerable<BankAccountInfoResponseDto>>();

            var value = ((IEnumerable<BankAccountInfoResponseDto>)result.Value).ToArray();
            value.Should().HaveCount(bankAccountInfoList.Count);

            var expected = bankAccountInfoList.ToArray();
            for (var index = 0; index < expected.Length; index++)
            {
                value[index].BankAccountID.Should().Be(expected[index].BankAccountID);
                value[index].AccountName.Should().Be(expected[index].AccountName);
            }
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
