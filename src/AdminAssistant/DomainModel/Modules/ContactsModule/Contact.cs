using AdminAssistant.Infra.DAL;

namespace AdminAssistant.DomainModel.Modules.ContactsModule
{
    public record Contact : IDatabasePersistable
    {
        public const int ContactFirstNameMaxLength = Constants.NameMaxLength;
        public const int ContactLastNameMaxLength = Constants.NameMaxLength;

        public int ContactID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public int PrimaryKey => ContactID;
    }
}
