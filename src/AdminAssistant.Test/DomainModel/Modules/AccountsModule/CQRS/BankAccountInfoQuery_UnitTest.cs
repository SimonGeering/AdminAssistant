#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Collections.Generic;
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
    public class BankAccountInfoQuery_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_BankAccountInfoList()
        {
            // Arrange
            var ownerID = 10;
            var bankAccountInfoList = new List<BankAccountInfo>()
            {
                Factory.BankAccountInfo.WithTestData(10).Build(),
                Factory.BankAccountInfo.WithTestData(20).Build()
            };

            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAdminAssistantServerSideDomainModel();

            var mockRepository = new Mock<IBankAccountInfoRepository>();
            mockRepository.Setup(x => x.GetListAsync()).Returns(Task.FromResult(bankAccountInfoList));

            services.AddTransient((sp) => mockRepository.Object);

            // Act
            var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new BankAccountInfoQuery(ownerID)).ConfigureAwait(false);

            // Assert
            result.Status.Should().Be(ResultStatus.Ok);
            result.Value.Should().BeEquivalentTo(bankAccountInfoList);            
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
