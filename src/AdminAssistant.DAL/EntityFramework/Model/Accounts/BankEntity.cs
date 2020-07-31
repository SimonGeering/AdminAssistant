using System;

namespace AdminAssistant.DAL.EntityFramework.Model.Accounts
{
    public class BankEntity
    {
        public int BankID { get; set; }
        public string BankName { get; set; } = string.Empty;
    }
}
