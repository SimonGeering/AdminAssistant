using System.Collections.Generic;

namespace AdminAssistant.Accounts.DAL.EntityFramework.Model
{
    public class OwnerEntity
    {
        public int OwnerID { get; set; }
        public int? CompanyID { get; set; }
        public int? PersonalDetailsID { get; set; }

        public CompanyEntity Company { get; set; } = null!;
        public PersonalDetailsEntity PersonalDetails { get; internal set; } = null!;
        public IList<BankAccountEntity> BankAccounts { get; internal set; } = new List<BankAccountEntity>();
    }
}
