namespace AdminAssistant.Infrastructure.EntityFramework.Model.Accounts;

public sealed class BankAccountTransactionDocumentEntity
{
    // Table "Accounts.BankAccountTransactionDocument" 
    public int BankAccountTransactionDocumentID { get; set; } // PK
    public int BankAccountTransactionID { get; set; }
    public int DocumentID { get; set; }
    public int AuditID { get; internal set; }
    public int PayeeID { get; internal set; }
    // Ref: "Accounts.BankAccountTransaction"."BankAccountTransactionID" < "Accounts.BankAccountTransactionDocument"."BankAccountTransactionDocumentID"
}
