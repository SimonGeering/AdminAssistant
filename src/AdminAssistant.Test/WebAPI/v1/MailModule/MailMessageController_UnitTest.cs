#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.MailModule;
using AdminAssistant.Modules.MailModule.Queries;
using AdminAssistant.WebAPI.v1.MailModule;
using Microsoft.AspNetCore.Mvc;
using MappingProfile = AdminAssistant.WebAPI.v1.MappingProfile;

namespace AdminAssistant.Test.WebAPI.v1.MailModule;

public sealed class MailMessageController_GetMailMessages
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_Status200OK_With_AListOfMailMessages_Given_NoArguments()
    {
        // Arrange
        var documents = new List<MailMessage>()
            {
                Factory.MailMessage.WithTestData(10).Build(),
                Factory.MailMessage.WithTestData(20).Build()
            };

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<MailMessageQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(Result<IEnumerable<MailMessage>>.Success(documents)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<MailMessageController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<MailMessageController>().GetMailMessages(default);

        // Assert
        response.Value.Should().BeNull();
        response.Result.Should().NotBeNull();
        response.Result.Should().BeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.Should().BeAssignableTo<IEnumerable<MailMessageResponseDto>>();

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
