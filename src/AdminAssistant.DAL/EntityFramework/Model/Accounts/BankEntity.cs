using System;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.DAL.EntityFramework.Model.Accounts
{
    public class BankEntity : IMapTo<Bank>
    {
        public int BankID { get; set; }
        public string BankName { get; set; } = string.Empty;
    }
}
