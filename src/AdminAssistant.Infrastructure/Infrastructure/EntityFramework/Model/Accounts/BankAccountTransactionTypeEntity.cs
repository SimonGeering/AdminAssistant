namespace AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;

public sealed class BankAccountTransactionTypeEntity
{
    // Table "Accounts.BankAccountTransactionType"
    public int BankAccountTransactionTypeID { get; set; } // PK
    public string Description { get; set; } = string.Empty;
    public bool AllowPersonal { get; set; }
    public bool AllowCompany { get; set; }
    public bool IsDeprecated { get; set; }
    // Ref: "Accounts.BankAccountTransactionType"."BankAccountTransactionTypeID" < "Accounts.BankAccountTransaction"."BankAccountTransactionTypeID"
}
