#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.TasksModule;
using AdminAssistant.DomainModel.Modules.TasksModule.CQRS;
using Ardalis.Result;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AdminAssistant.WebAPI.v1.TasksModule.TaskListControllerUnitTest
{
    public class GetTaskLists
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Return_Status200OK_With_AListOfToDoTask_Given_NoArguments()
        {
            // Arrange
            var taskLists = new List<TaskList>()
            {
                Factory.TaskList.WithTestData(10).Build(),
                Factory.TaskList.WithTestData(20).Build()
            };

            var services = new ServiceCollection();
            services.AddMockServerSideLogging();
            services.AddAutoMapper(typeof(MappingProfile));

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<TaskListQuery>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(Result<IEnumerable<TaskList>>.Success(taskLists)));

            services.AddTransient((sp) => mockMediator.Object);
            services.AddTransient<TaskListController>();

            // Act
            var response = await services.BuildServiceProvider().GetRequiredService<TaskListController>().GetTaskLists().ConfigureAwait(false);

            // Assert
            response.Value.Should().BeNull();
            response.Result.Should().NotBeNull();
            response.Result.Should().BeOfType<OkObjectResult>();

            var result = (OkObjectResult)response.Result!;
            result.Value.Should().BeAssignableTo<IEnumerable<TaskListResponseDto>>();

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
