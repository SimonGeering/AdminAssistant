#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.CalendarModule;
using AdminAssistant.Modules.CalendarModule.Queries;
using AdminAssistant.WebAPI.v1.CalendarModule;
using Microsoft.AspNetCore.Mvc;
using MappingProfile = AdminAssistant.WebAPI.v1.MappingProfile;

namespace AdminAssistant.Test.WebAPI.v1.CalendarModule;

public sealed class ReminderController_Get
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_Status200OK_With_AListOfAssets_Given_NoArguments()
    {
        // Arrange
        var documents = new List<Reminder>()
            {
                Factory.Reminder.WithTestData(10).Build(),
                Factory.Reminder.WithTestData(20).Build()
            };

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<ReminderQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(Result<IEnumerable<Reminder>>.Success(documents)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<ReminderController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<ReminderController>().GetReminders(default);

        // Assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.Should().BeAssignableTo<IEnumerable<ReminderResponseDto>>();

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
