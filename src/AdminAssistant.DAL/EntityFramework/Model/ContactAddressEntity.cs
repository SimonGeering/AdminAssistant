namespace AdminAssistant.Accounts.DAL.EntityFramework.Model
{
    public class ContactAddressEntity
    {
        public int ContactAddressID { get; set; }
        public int AddressID { get; set; }
        public int ContactID { get; internal set; }
    }
}
