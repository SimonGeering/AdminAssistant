using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts
{
    public class BankAccountTypeEntity : IMapTo<BankAccountType>
    {
        public int BankAccountTypeID { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool AllowPersonal { get; set; }
        public bool AllowCompany { get; set; }
        public bool IsDeprecated { get; set; }
    }
}
