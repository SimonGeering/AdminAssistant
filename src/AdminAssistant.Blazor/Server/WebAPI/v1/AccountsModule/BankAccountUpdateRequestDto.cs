using System;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.AccountsModule
{
    [SwaggerSchema(Required = new[] { "BankAccountID", "BankAccountTypeID", "CurrencyID", "AccountName", "IsBudgeted", "OpeningBalance", "OpenedOn" })]
    public class BankAccountUpdateRequestDto : IMapTo<BankAccount>
    {
        [SwaggerSchema("The BankAccount identifier.", ReadOnly = true)]
        public int BankAccountID { get; set; }
        [SwaggerSchema("The BankAccountType for this BankAccount.")]
        public int BankAccountTypeID { get; set; }
        public int CurrencyID { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public bool IsBudgeted { get; set; }
        public int OpeningBalance { get; set; }
        public int CurrentBalance { get; set; }
        public DateTime OpenedOn { get; set; }
    }
}
