using System;
using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

using Swashbuckle.AspNetCore.Annotations;

namespace AdminAssistant.WebAPI.v1.AccountsModule
{
    public record BankAccountResponseDto : IMapFrom<BankAccount>
    {
        [SwaggerSchema("The BankAccount identifier.", ReadOnly = true)]
        public int BankAccountID { get; init; }
        [SwaggerSchema("The BankAccountType for this BankAccount.")]
        public int BankAccountTypeID { get; init; }
        public int CurrencyID { get; init; }
        public string AccountName { get; init; } = string.Empty;
        public bool IsBudgeted { get; init; }
        public int OpeningBalance { get; init; }
        public int CurrentBalance { get; init; }
        public DateTime OpenedOn { get; init; }
    }
}
