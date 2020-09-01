using AdminAssistant.DomainModel.Modules.ContactsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.WebAPI.v1.ContactsModule
{
    public class ContactResponseDto : IMapFrom<Contact>
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
