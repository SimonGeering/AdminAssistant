using AdminAssistant.Infrastructure.EntityFramework.Model.Contacts;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infrastructure.EntityFramework.Model;

internal static class ContactsSchema
{
    private const string Name = "Contacts";

    internal static void OnModelCreating(ModelBuilder modelBuilder)
    {
        Address_OnModelCreating(modelBuilder);
        ContactAddress_OnModelCreating(modelBuilder);
        Contact_OnModelCreating(modelBuilder);

        // TODO: ContactsSchema.OnModelCreating
    }

    private static void Address_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressEntity>().ToTable("Address").Metadata.SetSchema(ContactsSchema.Name);
        modelBuilder.Entity<AddressEntity>().HasKey(x => x.AddressID);
        // TODO: Address_OnModelCreating
    }

    private static void ContactAddress_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactAddressEntity>().ToTable("ContactAddress").Metadata.SetSchema(ContactsSchema.Name);
        modelBuilder.Entity<ContactAddressEntity>().HasKey(x => x.ContactAddressID);
        // TODO: ContactAddress_OnModelCreating
    }
    private static void Contact_OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactEntity>().ToTable("Contact").Metadata.SetSchema(ContactsSchema.Name);
        modelBuilder.Entity<ContactEntity>().HasKey(x => x.ContactID);
        // TODO: Contact_OnModelCreating
    }
}
