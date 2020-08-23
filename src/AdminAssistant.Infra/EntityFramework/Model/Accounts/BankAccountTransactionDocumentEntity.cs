/*
Table "Accounts.BankAccountTransactionDocument" 
{
  "BankAccountTransactionDocumentID" INT[pk]
  "AuditID" INT
  "BankAccountTransactionID" INT
  "DocumentID" INT
}
Ref: "Accounts.BankAccountTransaction"."BankAccountTransactionID" < "Accounts.BankAccountTransactionDocument"."BankAccountTransactionDocumentID"

*/
namespace AdminAssistant.DAL.EntityFramework.Model.Accounts
{
    public class BankAccountTransactionDocumentEntity
    {
    }
}
