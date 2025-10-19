// ReSharper disable InconsistentNaming
#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.Domain;
using AdminAssistant.Modules.BudgetModule;
using AdminAssistant.Modules.BudgetModule.Queries;
using AdminAssistant.WebAPI.v1.BudgetModule;
using Microsoft.AspNetCore.Mvc;

namespace AdminAssistant.Test.WebAPI.v1.BudgetModule;

public sealed class BudgetController_UnitTest_Should
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

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<BudgetQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<IEnumerable<Budget>>.Success(budgets)));

        services.AddTransient(_ => mockMediator.Object);
        services.AddTransient<BudgetController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<BudgetController>().GetBudgets(CancellationToken.None);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.ShouldBeAssignableTo<IEnumerable<BudgetResponseDto>>();

        #pragma warning disable S125 // Sections of code should not be commented out
        //var value = ((IEnumerable<CurrencyResponseDto>)result.Value).ToArray();
        //value.Should().HaveCount(currencies.Count);

        //var expected = currencies.ToArray();
        //for (int i = 0; i < expected.Length; i++)
        //{
        //    value[i].CurrencyID.Should().Be(expected[i].CurrencyID);
        //    value[i].Symbol.Should().Be(expected[i].Symbol);
        //    value[i].DecimalFormat.Should().Be(expected[i].DecimalFormat);
        //}
        #pragma warning restore S125 // Sections of code should not be commented out
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
