#pragma warning disable CA1707 // Identifiers should not contain underscores
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.BudgetModule;
using AdminAssistant.DomainModel.Modules.BudgetModule.CQRS;
using Ardalis.Result;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant.WebAPI.v1.BudgetModule
{
    public class BudgetController_UnitTest_Should
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_Status200OK_With_AListOfBudgets_Given_NoArguments()
        {
            // Arrange
            var budgets = new List<Budget>()
            {
                Factory.Budget.WithTestData(10).Build(),
                Factory.Budget.WithTestData(20).Build()
            };

            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAutoMapper(typeof(MappingProfile));

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<BudgetQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<IEnumerable<Budget>>.Success(budgets)));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<BudgetController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<BudgetController>().GetBudgets().ConfigureAwait(false);

            // Assert
            response.Result.Should().BeOfType<OkObjectResult>();
            response.Value.Should().BeNull();

            var result = (OkObjectResult)response.Result;
            result.Value.Should().BeAssignableTo<IEnumerable<BudgetResponseDto>>();

            //var value = ((IEnumerable<CurrencyResponseDto>)result.Value).ToArray();
            //value.Should().HaveCount(currencies.Count);

            //var expected = currencies.ToArray();
            //for (int i = 0; i < expected.Length; i++)
            //{
            //    value[i].CurrencyID.Should().Be(expected[i].CurrencyID);
            //    value[i].Symbol.Should().Be(expected[i].Symbol);
            //    value[i].DecimalFormat.Should().Be(expected[i].DecimalFormat);
            //}
        }
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
