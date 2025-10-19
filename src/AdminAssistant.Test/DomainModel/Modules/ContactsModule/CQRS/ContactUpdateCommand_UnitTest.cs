#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Modules.ContactsModule.Commands;
using AdminAssistant.Modules.ContactsModule.Infrastructure.DAL;

namespace AdminAssistant.Test.DomainModel.Modules.ContactsModule.CQRS;

public sealed class ContactUpdateCommand_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task SaveAndReturn_APersistedContact_GivenAValidContact()
    {
        // Arrange
        var contact = Factory.Contact.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();

        var mockContactRepository = new Mock<IContactRepository>();
        mockContactRepository.Setup(x => x.SaveAsync(contact, It.IsAny<CancellationToken>()))
                             .Returns(Task.FromResult(contact));

        services.AddTransient((sp) => mockContactRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new ContactUpdateCommand(contact));

        // Assert
        result.ShouldNotBeNull();
        result.Status.ShouldBe(ResultStatus.Ok);
        result.ValidationErrors.ShouldBeEmpty();
        result.Value.ShouldNotBeNull();
        result.Value.ContactID.Value.ShouldBeGreaterThan(Constants.NewRecordID);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenAnInvalidContact()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddAdminAssistantApplication();
        services.AddTransient((sp) => new Mock<IContactRepository>().Object);

        var contact = Factory.Contact.WithTestData()
                                     .WithFirstName(string.Empty)
                                     .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new ContactUpdateCommand(contact));

        // Assert
        result.ShouldNotBeNull();
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ValidationErrors.ShouldNotBeEmpty();
    }
    // TODO: Add test for ContactUpdateCommand where ContactID not in IContactRepository
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
