using AdminAssistant.DomainModel.Modules.AccountsModule;
using AdminAssistant.Framework.TypeMapping;

namespace AdminAssistant.Infra.DAL.EntityFramework.Model.Accounts;

public sealed class PayeeEntity : IMapFrom<Payee>, IMapTo<Payee>
{
    // Table "Accounts.Payee"
    public int PayeeID { get; set; } // PK
    public int AuditID { get; internal set; }
    public string Name { get; set; } = string.Empty;

    public Core.AuditEntity Audit { get; internal set; } = null!;

    // Ref: "Accounts.Payee"."PayeeID" < "Accounts.BankAccountTransaction"."PayeeID"
    // Ref: "Accounts.Payee"."PayeeID" < "Accounts.PayeeContact"."PayeeID"
}
