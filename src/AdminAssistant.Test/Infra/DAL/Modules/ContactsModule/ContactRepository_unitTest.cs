#pragma warning disable CA1707 // Identifiers should not contain underscores
using AdminAssistant.Domain;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.EntityFramework.Model.Contacts;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Modules.ContactsModule;
using AdminAssistant.Modules.ContactsModule.Infrastructure.DAL;
using AdminAssistant.Shared;
using MappingProfile = AdminAssistant.Infrastructure.MappingProfile;

namespace AdminAssistant.Test.Infra.DAL.Modules.ContactsModule;

public sealed class ContactRepository_unitTest
{
    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_PopulatedContactList_WhenDatabaseHasData()
    {
        // Arrange
        var mapper = new ServiceCollection().AddAutoMapper(typeof(MappingProfile)).BuildServiceProvider().GetRequiredService<IMapper>();
        var contactList = new List<Contact>()
        {
            Factory.Contact.WithTestData(10).Build(),
            Factory.Contact.WithTestData(20).Build()
        };
        var data = mapper.Map<IList<ContactEntity>>(contactList);

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.Contacts)
            .Returns(data.BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient((sp) => new Mock<IDateTimeProvider>().Object);
        services.AddTransient((sp) => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra();
        services.AddTransient((sp) => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IContactRepository>().GetListAsync(default);

        // Assert
        result.Should().HaveCount(contactList.Count);
        result.Should().BeEquivalentTo(contactList);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Returns_AContact_WhenDatabaseContainsAnItemWithTheGivenID()
    {
        // Arrange
        var mapper = new ServiceCollection().AddAutoMapper(typeof(MappingProfile)).BuildServiceProvider().GetRequiredService<IMapper>();

        var contactList = new List<Contact>()
            {
                Factory.Contact.WithTestData(10).Build(),
                Factory.Contact.WithTestData(20).Build()
            };
        var data = mapper.Map<IList<ContactEntity>>(contactList);

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.Contacts)
            .Returns(data.BuildMockDbSet().Object);

        var services = new ServiceCollection();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient((sp) => new Mock<IDateTimeProvider>().Object);
        services.AddTransient((sp) => new Mock<IUserContextProvider>().Object);
        services.AddAdminAssistantServerSideInfra();
        services.AddTransient((sp) => mockDbContext.Object);

        // Act
        var result = await services.BuildServiceProvider().GetRequiredService<IContactRepository>().GetAsync(contactList[Constants.FirstItem].ContactID, default);

        // Assert
        result.Should().BeEquivalentTo(contactList[Constants.FirstItem]);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task AddsAuditing_WhenSavingNewContact()
    {
        // Arrange
        var mapper = new ServiceCollection().AddAutoMapper(typeof(MappingProfile)).BuildServiceProvider().GetRequiredService<IMapper>();
        var contactList = new List<Contact>()
            {
                Factory.Contact.WithTestData(10).Build(),
                Factory.Contact.WithTestData(20).Build()
            };

        var mockContacts = mapper.Map<IList<ContactEntity>>(contactList).BuildMockDbSet();
        mockContacts.Setup(x => x.Add(It.IsAny<ContactEntity>()));

        var mockDbContext = new Mock<IApplicationDbContext>();
        mockDbContext.Setup(x => x.Contacts).Returns(mockContacts.Object);

        var services = new ServiceCollection();
        services.AddMockDateTimeProvider();
        services.AddMockUserContextProvider();
        services.AddMockDbContext(mockDbContext);
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddAdminAssistantServerSideInfra();

        var newContactToSave = Factory.Contact.WithFirstName("TestNewContactToSaveFirstName")
                                              .WithLastName("TestNewContactToSaveLastName")
                                              .Build();

        // Act
        await services.BuildServiceProvider().GetRequiredService<IContactRepository>().SaveAsync(newContactToSave, default);

        // Assert
        mockContacts.Verify(x => x.Add(It.Is<ContactEntity>((arg) => IsValidForInsert(arg))), Times.Once());
        mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
    }

    //UpdatesAuditing_WhenSavingAnExistingContact

    private bool IsValidForInsert(ContactEntity contactToSave)
    {
        contactToSave.ContactID.Should().Be(Constants.NewRecordID);
        contactToSave.Audit.Should().NotBeNull();

        contactToSave.Audit.CreatedBy.Should().NotBeNullOrEmpty();
        contactToSave.Audit.CreatedOn.Should().NotBe(default);

        return true;
    }

    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "WIP")]
    private bool IsValidForUpdate(ContactEntity contactToSave)
    {
        contactToSave.ContactID.Should().NotBe(Constants.NewRecordID);
        contactToSave.Audit.Should().NotBeNull();

        return true;
    }
}
#pragma warning restore CA1707 // Identifiers should not contain underscores
