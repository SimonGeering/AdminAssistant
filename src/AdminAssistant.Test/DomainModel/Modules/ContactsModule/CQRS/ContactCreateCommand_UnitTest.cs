#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.DomainModel;
using AdminAssistant.DomainModel.Modules.ContactsModule.CQRS;
using AdminAssistant.Infra.DAL.Modules.ContactsModule;
using ObjectCloner.Extensions; // https://github.com/marcelltoth/ObjectCloner

namespace AdminAssistant.Test.DomainModel.Modules.ContactsModule.CQRS;

public sealed class ContactCreateCommand_Should
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_APersistedContact_GivenAValidContact()
    {
        // Arrange
        var contact = Factory.Contact.WithTestData().Build();

        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();

        var mockContactRepository = new Mock<IContactRepository>();
        mockContactRepository.Setup(x => x.SaveAsync(contact, It.IsAny<CancellationToken>()))
            .Returns(() =>
            {
                var result = contact.DeepClone();
                result = result with { ContactID = result.ContactID with { Value = 30 } };
                return Task.FromResult(result);
            });

        services.AddTransient((sp) => mockContactRepository.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new ContactCreateCommand(contact));

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Ok);
        result.ValidationErrors.Should().BeEmpty();
        result.Value.Should().NotBeNull();
        result.Value.ContactID.Value.Should().BeGreaterThan(Constants.NewRecordID);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Return_ValidationError_GivenAnInvalidContact()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddMockServerSideLogging();
        services.AddAdminAssistantServerSideDomainModel();
        services.AddTransient((sp) => new Mock<IContactRepository>().Object);

        var contact = Factory.Contact.WithTestData()
                                     .WithFirstName(string.Empty)
                                     .Build();
        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IMediator>().Send(new ContactCreateCommand(contact));

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ValidationErrors.Should().NotBeEmpty();
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
