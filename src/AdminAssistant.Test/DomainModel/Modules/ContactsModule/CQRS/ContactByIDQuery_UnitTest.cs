#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Infra.DAL.Modules.ContactsModule;
using AdminAssistant.Modules.ContactsModule;
using AdminAssistant.Modules.ContactsModule.Queries;

namespace AdminAssistant.Test.DomainModel.Modules.ContactsModule.CQRS;

public sealed class ContactByIDQuery_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_NotFound_GivenANonExistentContactID()
    {
        // Arrange
        var nonExistentContactID = ContactId.Default;

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantApplication();
        services.AddAdminAssistantServerSideDomainModel();

        var mockContactRepository = new Mock<IContactRepository>();
        mockContactRepository.Setup(x => x.GetAsync(nonExistentContactID, It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Contact?>(null!));

        services.AddTransient((sp) => mockContactRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new ContactByIDQuery(nonExistentContactID.Value));

        // Assert
        result.Status.Should().Be(ResultStatus.NotFound);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_OKContact_GivenAnExistingContactID()
    {
        // Arrange
        var contact = Factory.Contact.WithTestData(10).Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantApplication();
        services.AddAdminAssistantServerSideDomainModel();

        var mockContactRepository = new Mock<IContactRepository>();
        mockContactRepository.Setup(x => x.GetAsync(contact.ContactID, It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Contact?>(contact));

        services.AddTransient((sp) => mockContactRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new ContactByIDQuery(contact.ContactID.Value));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(contact);
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
