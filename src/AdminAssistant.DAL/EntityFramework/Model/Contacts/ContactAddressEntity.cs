namespace AdminAssistant.DAL.EntityFramework.Model.Contacts
{
    public class ContactAddressEntity
    {
        public int ContactAddressID { get; set; }
        public int AddressID { get; set; }
        public int ContactID { get; internal set; }
    }
}
