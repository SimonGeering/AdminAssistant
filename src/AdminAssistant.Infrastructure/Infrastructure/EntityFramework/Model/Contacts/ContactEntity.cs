using AdminAssistant.Modules.ContactsModule;

namespace AdminAssistant.Infrastructure.EntityFramework.Model.Contacts;

public sealed class ContactEntity : IMapFrom<Contact>, IMapTo<Contact>
{
    // Table "Contacts.Contact"
    public int ContactID { get; set; } // PK
    public int AuditID { get; internal set; }
    public int OwnerID { get; set; }
    public int TitleID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }

    public Core.AuditEntity Audit { get; internal set; } = null!;
    public Core.OwnerEntity Owner { get; internal set; } = null!;

    public void MapFrom(AutoMapper.Profile profile) => profile
        .CreateMap<Contact, ContactEntity>()
        .ForMember(x => x.AuditID, opt => opt.Ignore())
        .ForMember(x => x.Audit, opt => opt.Ignore())
        .ForMember(x => x.OwnerID, opt => opt.Ignore())
        .ForMember(x => x.Owner, opt => opt.Ignore());

    // Ref: "Contacts.Contact"."ContactID" < "Contacts.ContactAddress"."ContactID"
    // Ref: "Contacts.Contact"."ContactID" < "Accounts.PayeeContact"."ContactID"
}
