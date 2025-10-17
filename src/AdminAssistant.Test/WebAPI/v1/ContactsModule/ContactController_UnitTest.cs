#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.ContactsModule;
using AdminAssistant.Modules.ContactsModule.Queries;
using AdminAssistant.WebAPI.v1.ContactsModule;
using Microsoft.AspNetCore.Mvc;
using MappingProfile = AdminAssistant.WebAPI.v1.MappingProfile;

namespace AdminAssistant.Test.WebAPI.v1.ContactsModule;

public sealed class ContactController_GetContacts
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_Status200OK_With_AListOfContacts_Given_NoArguments()
    {
        // Arrange
        var contacts = new List<Contact>()
        {
            Factory.Contact.WithTestData(10).Build(),
            Factory.Contact.WithTestData(20).Build()
        };

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAutoMapper(typeof(MappingProfile));

        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<ContactQuery>(), It.IsAny<CancellationToken>()))
                    .Returns(ValueTask.FromResult(Result<IEnumerable<Contact>>.Success(contacts)));

        services.AddTransient((sp) => mockMediator.Object);
        services.AddTransient<ContactController>();

        // Act
        var response = await services.BuildServiceProvider().GetRequiredService<ContactController>().GetContact(default);

        // Assert
        response.Value.ShouldBeNull();
        response.Result.ShouldNotBeNull();
        response.Result.ShouldBeOfType<OkObjectResult>();

        var result = (OkObjectResult)response.Result!;
        result.Value.ShouldBeAssignableTo<IEnumerable<ContactResponseDto>>();

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

    #pragma warning disable S125 // Sections of code should not be commented out
    // [Fact]
    // [Trait("Category", "Unit")]
    // public async Task Returns_Status200OK_With_ASingleContact_Given_AValidContactID()
    // {
    //// Arrange
    //var contacts = new List<Contact>()
    //{
    //    Factory.Contact.WithTestData(10).Build(),
    //    Factory.Contact.WithTestData(20).Build()
    //};
    //var validContactId = contacts[1].ContactID;

    //var services = new ServiceCollection();
    //services.AddMockServerSideLogging();
    //services.AddAutoMapper(typeof(MappingProfile));

    //var mockMediator = new Mock<IMediator>();
    //services.AddTransient((sp) => mockMediator.Object);

    //services.AddTransient<ContactController>();

    //// Act
    //var response = await services.BuildServiceProvider().GetRequiredService<ContactController>().ContactGetById(validContactId).ConfigureAwait(false);

    //// Assert
    //response.Value.Should().BeNull();
    //response.Result.Should().NotBeNull();
    //response.Result.Should().BeOfType<OkObjectResult>();

    //var result = (OkObjectResult)response.Result!;
    //result.Value.Should().BeAssignableTo<ContactResponseDto>();
    //}
    #pragma warning restore S125 // Sections of code should not be commented out
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
