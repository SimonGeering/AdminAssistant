using AdminAssistant.DAL.EntityFramework.Model.Contacts;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.DAL.EntityFramework.Model
{
    internal class ContactsSchema
    {
        private const string Name = "Contacts";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1801:Review unused parameters", Justification = "WIP")]
        internal static void OnModelCreating(ModelBuilder modelBuilder)
        {
            ContactsSchema.Address_OnModelCreating(modelBuilder);
            ContactsSchema.ContactAddress_OnModelCreating(modelBuilder);
            ContactsSchema.Contact_OnModelCreating(modelBuilder);

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
}
