// ReSharper disable InconsistentNaming
#pragma warning disable CA1707 // Identifiers should not contain underscores

using AdminAssistant.Domain;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.EntityFramework.Model.Contacts;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Modules.ContactsModule;
using AdminAssistant.Modules.ContactsModule.Infrastructure.DAL;
using AdminAssistant.Shared;

namespace AdminAssistant.Test.Infra.DAL.Modules.ContactsModule;

public sealed class ContactRepository_unitTest
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_PopulatedContactList_WhenDatabaseHasData()
    {
        // Arrange
        var contactList = new List<Contact>()
        {
            Factory.Contact.WithTestData(10).Build(),
            Factory.Contact.WithTestData(20).Build()
        };

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.Contacts)
            .Returns(contactList.ToContactEntityList().BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddTransient(_ => new Mock<IDateTimeProvider>().Object);
        services.AddTransient(_ => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra();
        services.AddTransient(_ => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IContactRepository>().GetListAsync(CancellationToken.None);

        // Assert
        result.Count.ShouldBe(contactList.Count);
        result.ShouldBeEquivalentTo(contactList);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_AContact_WhenDatabaseContainsAnItemWithTheGivenID()
    {
        // Arrange
        var contactList = new List<Contact>()
            {
                Factory.Contact.WithTestData(10).Build(),
                Factory.Contact.WithTestData(20).Build()
            };

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.Contacts)
            .Returns(contactList.ToContactEntityList().BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddTransient(_ => new Mock<IDateTimeProvider>().Object);
        services.AddTransient(_ => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra();
        services.AddTransient(_ => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IContactRepository>().GetAsync(contactList[Constants.FirstItem].ContactID, CancellationToken.None);

        // Assert
        result.ShouldBeEquivalentTo(contactList[Constants.FirstItem]);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task AddsAuditing_WhenSavingNewContact()
    {
        // Arrange
        var contactList = new List<Contact>()
            {
                Factory.Contact.WithTestData(10).Build(),
                Factory.Contact.WithTestData(20).Build()
            };

        var mockContacts = contactList.ToContactEntityList().BuildMockDbSet();
        mockContacts.Setup(x => x.Add(It.IsAny<ContactEntity>()));

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.Contacts).Returns(mockContacts.Object);

        var services = new ServiceCollection();
        services.AddMockDateTimeProvider();
        services.AddMockUserContextProvider();
        services.AddMockDbContext(mockDbContext);
        services.AddAdminAssistantServerSideInfra();

        var newContactToSave = Factory.Contact.WithFirstName("TestNewContactToSaveFirstName")
                                              .WithLastName("TestNewContactToSaveLastName")
                                              .Build();

        // Act
        await services.BuildServiceProvider().GetRequiredService<IContactRepository>().SaveAsync(newContactToSave, CancellationToken.None);

        // Assert
        mockContacts.Verify(x => x.Add(It.Is<ContactEntity>(arg => IsValidForInsert(arg))), Times.Once());
        mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    //UpdatesAuditing_WhenSavingAnExistingContact

    private static bool IsValidForInsert(ContactEntity contactToSave)
    {
        contactToSave.ContactID.ShouldBe(Constants.NewRecordID);
        contactToSave.Audit.ShouldNotBeNull();

        contactToSave.Audit.CreatedBy.ShouldNotBeNullOrEmpty();
        contactToSave.Audit.CreatedOn.ShouldNotBe(default);

        return true;
    }

    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "WIP")]
    private static bool IsValidForUpdate(ContactEntity contactToSave)
    {
        contactToSave.ContactID.ShouldNotBe(Constants.NewRecordID);
        contactToSave.Audit.ShouldNotBeNull();

        return true;
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
